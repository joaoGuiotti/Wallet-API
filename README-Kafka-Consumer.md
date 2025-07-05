# Kafka Consumer para TransactionCreatedEvent

## Visão Geral

Este documento explica como implementar e testar o consumer de Kafka para eventos `TransactionCreatedEvent` no projeto Wallet.

## Componentes Implementados

### 1. **IKafkaConsumer** 
Interface para o consumer de Kafka localizada em `Wallet.Application\Interfaces\Kafka\IKafkaConsumer.cs`

### 2. **KafkaConsumer**
Implementação do consumer em `Wallet.Infrastructure\Kafka\KafkaConsumer.cs` que:
- Consome mensagens do tópico `transaction-created`
- Deserializa eventos `TransactionCreatedEvent`
- Processa os eventos com logging detalhado
- Gerencia commits manuais para garantir processamento

### 3. **KafkaConsumerBackgroundService**
Background service em `Wallet.Infrastructure\Kafka\KafkaConsumerBackgroundService.cs` que executa o consumer continuamente

### 4. **KafkaTestController**
Controller de teste em `Wallet.API\Controllers\KafkaTestController.cs` com endpoints para:
- Enviar eventos únicos
- Enviar múltiplos eventos para teste

## Configurações

### appsettings.json
```json
{
  "Kafka": {
    "ProducerSettings": {
      "BootstrapServers": "kafka:9092",
      "ClientId": "wallet-api"
    },
    "ConsumerSettings": {
      "BootstrapServers": "kafka:9092",
      "GroupId": "wallet-consumer-group",
      "ClientId": "wallet-consumer"
    },
    "Topics": {
      "Transactions": "transactions"
    }
  }
}
```

### appsettings.Development.json
```json
{
  "Kafka": {
    "ProducerSettings": {
      "BootstrapServers": "localhost:9092",
      "ClientId": "wallet-api-dev"
    },
    "ConsumerSettings": {
      "BootstrapServers": "localhost:9092",
      "GroupId": "wallet-consumer-group-dev",
      "ClientId": "wallet-consumer-dev"
    },
    "Topics": {
      "Transactions": "transactions"
    }
  }
}
```

## Como Testar

### Pré-requisitos
1. Kafka rodando (localhost:9092 para desenvolvimento ou kafka:9092 para Docker)
2. Aplicação Wallet.API rodando
3. **Confluent Control Center disponível em http://localhost:9021** (opcional, mas recomendado)

### Monitoramento com Control Center

Para uma experiência completa de monitoramento, acesse o Confluent Control Center:
- **URL:** http://localhost:9021
- **Ver tópicos:** Topics → transactions
- **Monitorar consumer:** Consumers → wallet-consumer-group
- **Inspecionar mensagens:** Topics → transactions → Messages

*Consulte o arquivo `README-Control-Center.md` para instruções detalhadas.*

### Através de transações reais

Quando uma transação é criada no sistema e o método `Commit()` é chamado, automaticamente um evento será publicado no Kafka e consumido pelo consumer.

## Logs Esperados

Quando o consumer processar um evento, você verá logs similares a:

```
info: Wallet.Infrastructure.Kafka.KafkaConsumer[0]
      Received message: {"TransactionId":"123e4567-e89b-12d3-a456-426614174000","CreatedAt":"2025-07-04T10:30:00Z"}

info: Wallet.Infrastructure.Kafka.KafkaConsumer[0]
      Processing TransactionCreatedEvent for Transaction ID: 123e4567-e89b-12d3-a456-426614174000

info: Wallet.Infrastructure.Kafka.KafkaConsumer[0]
      🎉 Transaction 123e4567-e89b-12d3-a456-426614174000 was created successfully!

info: Wallet.Infrastructure.Kafka.KafkaConsumer[0]
      ✅ Transaction 123e4567-e89b-12d3-a456-426614174000 event processed successfully!
```

## Personalizações

### Adicionar Nova Lógica de Processamento

No método `ProcessTransactionCreatedEvent` em `KafkaConsumer.cs`, você pode adicionar:

- Envio de notificações
- Atualização de caches
- Integração com sistemas externos
- Validações adicionais
- Persistência em bancos de dados
- Chamadas para outros serviços

### Configurar Diferentes Tópicos

Modifique a configuração `Kafka:Topics:Transactions` no appsettings.json para usar diferentes tópicos por ambiente.

### Ajustar Configurações do Consumer

No `CreateConsumerBuild` você pode ajustar:
- `AutoOffsetReset`: Earliest, Latest
- `EnableAutoCommit`: true/false
- `GroupId`: para diferentes grupos de consumo
- Timeouts e outras configurações do Confluent.Kafka

## Troubleshooting

### Consumer não está recebendo mensagens
1. Verifique se o Kafka está rodando
2. Confirme as configurações de BootstrapServers
3. Verifique se o tópico existe
4. Confirme o GroupId do consumer

### Erros de deserialização
1. Verifique se a estrutura JSON está correta
2. Confirme se as propriedades do evento correspondem
3. Analise os logs de erro para detalhes específicos

### Consumer não está iniciando
1. Verifique se o background service está registrado
2. Confirme as dependências no ServiceExtensions
3. Analise os logs de inicialização da aplicação
