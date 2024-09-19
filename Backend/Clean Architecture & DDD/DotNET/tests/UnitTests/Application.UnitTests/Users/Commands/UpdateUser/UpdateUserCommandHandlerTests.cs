using Application.UnitTests.TestUtils.Users;
using Application.Users.Commands.UpdateUser;
using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Events;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using ErrorOr;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Users.Commands.UpdateUser;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public UpdateUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new UpdateUserCommand(
            user.Id.Value,
            user.Email.Value,
            user.PhoneNumber.Value,
            user.FirstName,
            user.LastName,
            null);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new UpdateUserCommand(
            user.Id.Value,
            user.Email.Value,
            user.PhoneNumber.Value,
            user.FirstName,
            user.LastName,
            null);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(user);
    }

    [Fact]
    public async Task Handle_Should_CallUpdateUserInRepository_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new UpdateUserCommand(
            user.Id.Value,
            user.Email.Value,
            user.PhoneNumber.Value,
            user.FirstName,
            user.LastName,
            null);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Verify(
            x => x.UpdateUser(It.IsAny<User>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_RaiseUserUpdatedDomainEvent_WhenUserFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new UpdateUserCommand(
            user.Id.Value,
            user.Email.Value,
            user.PhoneNumber.Value,
            user.FirstName,
            user.LastName,
            null);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        user.GetDomainEvents()
            .Should()
            .ContainSingle().Which
            .Should()
            .BeOfType<UserUpdatedDomainEvent>();
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenUserNotFound()
    {
        var user = UserFactory.CreateUser();

        // Arrange
        var command = new UpdateUserCommand(
            user.Id.Value,
            user.Email.Value,
            user.PhoneNumber.Value,
            user.FirstName,
            user.LastName,
            null);

        _userRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
