namespace Infrastructure.Persistence.Constants;

/// <summary>
/// A static class containing constant table names used in the database contexts.
/// </summary>
internal static class TableNames
{
    // AuthenticationDbContext
    internal const string Roles = nameof(Roles);
    internal const string RoleClaims = nameof(RoleClaims);
    internal const string UserClaims = nameof(UserClaims);
    internal const string UserLogins = nameof(UserLogins);
    internal const string UserRoles = nameof(UserRoles);
    internal const string UserTokens = nameof(UserTokens);

    // AuthenticationDbContext & DotNETDbContext
    internal const string Users = nameof(Users);

    // DotNETDbContext
    internal const string OutboxMessages = nameof(OutboxMessages);
    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);
}
