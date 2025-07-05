using Moq;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Client.GetByIdClient;
using Wallet.Domain.Entities;

namespace Wallet.Tests.Units.UseCases;

public class GetByIdClientUseCaseTests
{

    //[Fact]
    //public async Task ExecuteAsync_ReturnsClient_WhenClientExists()
    //{
    //    // Arrange
    //    var clientId = new Guid().ToString();
    //    var client = new Client(clientId, "Test","test@email.com");

    //    var repoMock = new Mock<IClientRepository>();
    //    repoMock.Setup(r => r.Find(clientId)).ReturnsAsync(client);

    //    var useCase = new GetByIdClientUseCase(repoMock.Object);

    //    // Act
    //    var result = await useCase.ExecuteAsync(clientId, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(clientId, result.Id);
    //}

}
