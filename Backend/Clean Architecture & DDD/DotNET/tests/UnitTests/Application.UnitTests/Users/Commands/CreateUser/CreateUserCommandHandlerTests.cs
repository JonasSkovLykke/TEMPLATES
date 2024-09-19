using Application.UnitTests.TestUtils.TestConstants;
using Application.Users.Commands.CreateUser;
using Domain.Common.Persistence;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using ErrorOr;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Users.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IIdentityUserRepository> _identityUserRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateUserCommandHandlerTests()
    {
        _identityUserRepositoryMock = new();
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailNotUnique()
    {
        // Arrange
        var command = new CreateUserCommand(
           Constants.User.Email.Value,
           Constants.User.Password,
           Constants.User.FirstName,
           Constants.User.LastName,
           Constants.User.PhoneNumber.Value);

        _identityUserRepositoryMock.Setup(
                x => x.ExistsByEmailAsync(
                    It.IsAny<string>()))
            .ReturnsAsync(true);

        var handler = new CreateUserCommandHandler(
            _identityUserRepositoryMock.Object,
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(UserErrors.DuplicateEmail);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenEmailIsUnique()
    {
        // Arrange
        var command = new CreateUserCommand(
           Constants.User.Email.Value,
           Constants.User.Password,
           Constants.User.FirstName,
           Constants.User.LastName,
           Constants.User.PhoneNumber.Value);

        _identityUserRepositoryMock.Setup(
                x => x.ExistsByEmailAsync(
                    It.IsAny<string>()))
            .ReturnsAsync(false);

        _identityUserRepositoryMock.Setup(
                x => x.CreateIdentityUserAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Constants.User.IdentityUserId);

        var handler = new CreateUserCommandHandler(
            _identityUserRepositoryMock.Object,
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_CallAddInRepository_WhenEmailIsUnique()
    {
        // Arrange
        var command = new CreateUserCommand(
           Constants.User.Email.Value,
           Constants.User.Password,
           Constants.User.FirstName,
           Constants.User.LastName,
           Constants.User.PhoneNumber.Value);

        _identityUserRepositoryMock.Setup(
                x => x.ExistsByEmailAsync(
                    It.IsAny<string>()))
            .ReturnsAsync(false);

        _identityUserRepositoryMock.Setup(
                x => x.CreateIdentityUserAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(Constants.User.IdentityUserId);

        var handler = new CreateUserCommandHandler(
            _identityUserRepositoryMock.Object,
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<User> result = await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Verify(
            x => x.AddUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new CreateUserCommand(
           Constants.User.Email.Value,
           Constants.User.Password,
           Constants.User.FirstName,
           Constants.User.LastName,
           Constants.User.PhoneNumber.Value);

        _identityUserRepositoryMock.Setup(
                x => x.ExistsByEmailAsync(
                    It.IsAny<string>()))
            .ReturnsAsync(true);

        var handler = new CreateUserCommandHandler(
            _identityUserRepositoryMock.Object,
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
