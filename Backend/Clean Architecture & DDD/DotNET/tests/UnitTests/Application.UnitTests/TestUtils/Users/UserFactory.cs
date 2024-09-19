using Application.UnitTests.TestUtils.TestConstants;
using Domain.UserAggregate;

namespace Application.UnitTests.TestUtils.Users;

internal static class UserFactory
{
    public static User CreateUser() =>
            User.Create(
                Constants.User.IdentityUserId,
                Constants.User.Email,
                Constants.User.PhoneNumber,
                Constants.User.FirstName,
                Constants.User.LastName);
}
