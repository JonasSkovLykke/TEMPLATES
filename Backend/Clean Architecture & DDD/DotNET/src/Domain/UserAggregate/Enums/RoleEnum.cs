using SharedKernel.Primitives;

namespace Domain.UserAggregate.Entities;

/// <summary>
/// Represents a role within the user aggregate.
/// </summary>
public abstract class RoleEnum : Enumeration<RoleEnum>
{
    public static readonly RoleEnum Administrator = new AdministratorRole();
    public static readonly RoleEnum User = new UserRole();
    public static readonly RoleEnum Guest = new GuestRole();

    protected RoleEnum(int value, string name)
        : base(value, name)
    {
    }

    /// <summary>
    /// Represents the Administrator role.
    /// </summary>
    private sealed class AdministratorRole : RoleEnum
    {
        public AdministratorRole()
            : base(1, nameof(Administrator))
        {
        }
    }

    /// <summary>
    /// Represents the User role.
    /// </summary>
    private sealed class UserRole : RoleEnum
    {
        public UserRole()
            : base(2, nameof(User))
        {
        }
    }

    /// <summary>
    /// Represents the Guest role.
    /// </summary>
    private sealed class GuestRole : RoleEnum
    {
        public GuestRole()
            : base(3, nameof(Guest))
        {
        }
    }
}
