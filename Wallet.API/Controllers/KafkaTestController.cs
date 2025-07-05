using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wallet.Application.Kafka;
using Wallet.Domain.Events;

namespace Wallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KafkaTestController : ControllerBase
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly ILogger<KafkaTestController> _logger;
    private readonly IConfiguration _configuration;

    public KafkaTestController(
        IKafkaProducer kafkaProducer,
        ILogger<KafkaTestController> logger,
        IConfiguration configuration)
    {
        _kafkaProducer = kafkaProducer;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost("send-transaction-created-event")]
    public async Task<IActionResult> SendTransactionCreatedEvent([FromBody] SendTransactionEventRequest request)
    {
        try
        {
            var transactionEvent = new TransactionCreatedEvent(request.TransactionId ?? Guid.NewGuid());
            var topic = _configuration.GetValue<string>("Kafka:Topics:TransactionCreated") ?? "transaction-created";

            var message = JsonSerializer.Serialize(transactionEvent);

            _logger.LogInformation($"Sending TransactionCreatedEvent to topic {topic}: {message}");

            await _kafkaProducer.ProduceAsync(topic, message, CancellationToken.None);

            return Ok(new
            {
                Message = "Event sent successfully",
                TransactionId = transactionEvent.TransactionId,
                Topic = topic,
                Timestamp = transactionEvent.OccuredAt
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending TransactionCreatedEvent");
            return StatusCode(500, new { Error = "Failed to send event", Details = ex.Message });
        }
    }

    [HttpPost("send-multiple-events/{count}")]
    public async Task<IActionResult> SendMultipleEvents(int count)
    {
        try
        {
            var events = new List<object>();
            var topic = _configuration.GetValue<string>("Kafka:Topics:TransactionCreated") ?? "transaction-created";

            for (int i = 0; i < count; i++)
            {
                var transactionEvent = new TransactionCreatedEvent(Guid.NewGuid());
                var message = JsonSerializer.Serialize(transactionEvent);

                await _kafkaProducer.ProduceAsync(topic, message, CancellationToken.None);

                events.Add(new
                {
                    TransactionId = transactionEvent.TransactionId,
                    Timestamp = transactionEvent.OccuredAt
                });

                // Pequeno delay para simular eventos em momentos diferentes
                await Task.Delay(100);
            }

            _logger.LogInformation($"Sent {count} TransactionCreatedEvents to topic {topic}");

            return Ok(new
            {
                Message = $"Successfully sent {count} events",
                Events = events,
                Topic = topic
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending multiple TransactionCreatedEvents");
            return StatusCode(500, new { Error = "Failed to send events", Details = ex.Message });
        }
    }
}

public class SendTransactionEventRequest
{
    public Guid? TransactionId { get; set; }
}
