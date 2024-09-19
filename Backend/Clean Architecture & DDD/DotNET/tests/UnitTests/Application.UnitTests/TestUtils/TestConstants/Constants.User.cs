using Domain.UserAggregate.ValueObjects;
using SharedKernel.ValueObjects;

namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class User
    {
        public const int IdentityUserId = 0;
        public static readonly UserId Id = UserId.Create();
        public static readonly Email Email = Email.Create("john@doe.com").Value;
        public static readonly PhoneNumber PhoneNumber = PhoneNumber.Create("12345678");
        public const string Password = "P@55w0rd";
        public const string FirstName = "John";
        public const string LastName = "Doe";
    }
}
