using Application.UnitTests.TestUtils.TestConstants;
using Application.UnitTests.TestUtils.Users;
using Application.Users.Commands.DeleteUser;
using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Events;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Users.Commands.DeleteUser;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        // Arrange
        var command = new DeleteUserCommand(Constants.User.Id.Value);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handler = new DeleteUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<Deleted> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new DeleteUserCommand(user.Id.Value);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object,
                                                   _unitOfWorkMock.Object);

        // Act
        ErrorOr<Deleted> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Deleted);
    }

    [Fact]
    public async Task Handle_Should_CallDeleteUserInRepository_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new DeleteUserCommand(user.Id.Value);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object,
                                                   _unitOfWorkMock.Object);

        // Act
        ErrorOr<Deleted> result = await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Verify(
            x => x.DeleteUser(It.IsAny<User>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_RaiseUserDeletedDomainEvent_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new DeleteUserCommand(user.Id.Value);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object,
                                                   _unitOfWorkMock.Object);

        // Act
        ErrorOr<Deleted> result = await handler.Handle(command, default);

        // Assert
        user.GetDomainEvents()
            .Should()
            .ContainSingle().Which
            .Should()
            .BeOfType<UserDeletedDomainEvent>();
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenUserNotFound()
    {
        // Arrange
        var command = new DeleteUserCommand(Constants.User.Id.Value);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handler = new DeleteUserCommandHandler(_userRepositoryMock.Object,
                                                   _unitOfWorkMock.Object);

        // Act
        ErrorOr<Deleted> result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
