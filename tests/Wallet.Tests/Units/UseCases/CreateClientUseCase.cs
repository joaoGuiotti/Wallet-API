
namespace Wallet.Tests.Units.UseCases;

using AutoMapper;
using Moq;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Client.CreateClient;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;

public class CreateClientUseCaseTests
{
    //[Fact]
    //public async Task ExecuteAsync_ShouldCreateClient_WhenEmailIsUnique()
    //{
    //    // Arrange
    //    var mockRepo = new Mock<IClientRepository>();
    //    var mockUow = new Mock<IUnitOfWork>();
    //    var mockMapper = new Mock<IMapper>();
    //    var cancellationToken = CancellationToken.None;

    //    mockRepo.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), cancellationToken)).Returns(Task.FromResult(true));
    //    mockRepo.Setup(r => r.Create(It.IsAny<Client>(), cancellationToken)).Returns(Task.CompletedTask);
    //    mockUow.Setup(u => u.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

    //    var client = new Client(Guid.NewGuid().ToString(), "Test Name", "test@email.com");
    //    var expectedResult = new CreateClientResul(client.Id, client.Name, client.Email, client.CreatedAt);

    //    mockMapper.Setup(m => m.Map<CreateClientResult>(It.IsAny<Client>())).Returns(expectedResult);

    //    var useCase = new CreateClientUseCase(mockRepo.Object, mockMapper.Object, mockUow.Object);
    //    var command = new CreateClientCommand("Test Name", "test@email.com");

    //    // Act
    //    var result = await useCase.ExecuteAsync(command);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(expectedResult.Id, result.Id);
    //    Assert.Equal(expectedResult.Name, result.Name);
    //    Assert.Equal(expectedResult.Email, result.Email);
    //    mockRepo.Verify(r => r.Create(It.IsAny<Client>()), Times.Once);
    //    mockUow.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
    //    mockMapper.Verify(m => m.Map<CreateClientResult>(It.IsAny<Client>()), Times.Once);
    //}

    //[Fact]
    //public async Task ExecuteAsync_ShouldThrowException_WhenEmailAlreadyExists()
    //{
    //    // Arrange
    //    var mockRepo = new Mock<IClientRepository>();
    //    var mockUow = new Mock<IUnitOfWork>();
    //    var mockMapper = new Mock<IMapper>();

    //    mockRepo.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(true);

    //    var useCase = new CreateClientUseCase(mockRepo.Object, mockMapper.Object, mockUow.Object);
    //    var command = new CreateClientCommand("Test Name", "test@email.com");

    //    // Act & Assert
    //    var ex = await Assert.ThrowsAsync<ApplicationException>(() =>
    //        useCase.ExecuteAsync(command)
    //    );
    //    Assert.Equal("Email already in use", ex.Message);
    //    mockRepo.Verify(r => r.Create(It.IsAny<Client>()), Times.Never);
    //    mockUow.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Never);
    //    mockMapper.Verify(m => m.Map<CreateClientResult>(It.IsAny<Client>()), Times.Never);
    //}
}