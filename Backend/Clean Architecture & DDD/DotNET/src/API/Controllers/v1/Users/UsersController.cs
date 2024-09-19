using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.Register;
using Application.Users.Commands.UpdateUser;
using Application.Users.Common;
using Application.Users.Queries.GetUser;
using Application.Users.Queries.GetUserByEmail;
using Contracts.Users;
using Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.v1.Users;

public class UsersController : BaseControllerV1
{
    // GET: v1/Users
    /// <summary>
    /// Get a list of users asynchronously.
    /// </summary>
    [HttpGet]
    [ActionName(nameof(GetUsersAsync))]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();

        ErrorOr<IEnumerable<User>> results = await Sender.Send(query, cancellationToken);

        return results.Match(
            users => base.Ok(users.Select(user => Mapper.Map<UserResponse>(user))),
            Problem);
    }

    // GET: v1/Users/{id}
    /// <summary>
    /// Get a user by ID asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    [HttpGet("{id:Guid}")]
    [ActionName(nameof(GetUserAsync))]
    [ProducesResponseType(200, Type = typeof(UserWithRolesResponse))]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);

        ErrorOr<UserResult> result = await Sender.Send(query, cancellationToken);

        return result.Match(
            userResult => base.Ok(Mapper.Map<UserWithRolesResponse>(userResult)),
            Problem);
    }

    // POST: v1/Users
    /// <summary>
    /// Create a new user asynchronously.
    /// </summary>
    /// <param name="request">The request data for creating a user.</param>
    [HttpPost]
    [ActionName(nameof(CreateUserAsync))]
    [ProducesResponseType(201, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateUserCommand>(request);

        ErrorOr<User> result = await Sender.Send(command, cancellationToken);

        return result.Match(
            user => base.CreatedAtAction(
                actionName: nameof(GetUserAsync),
                routeValues: new { id = user.Id.Value },
                value: Mapper.Map<UserResponse>(user)),
            Problem);
    }

    // PUT: v1/Users/{id}
    /// <summary>
    /// Update an existing user asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="request">The request data for updating the user.</param>
    [HttpPut("{id:Guid}")]
    [ActionName(nameof(UpdateUserAsync))]
    [ProducesResponseType(201, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<UpdateUserCommand>((id, request));

        ErrorOr<User> result = await Sender.Send(command, cancellationToken);

        return result.Match(
            user => base.Ok(Mapper.Map<UserWithRolesResponse>(user)),
            Problem);
    }

    // DELETE: v1/Users/{id}
    /// <summary>
    /// Delete a user by ID asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    [HttpDelete("{id:Guid}")]
    [ActionName(nameof(DeleteUserAsync))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);

        ErrorOr<Deleted> result = await Sender.Send(command, cancellationToken);

        return result.Match(
            _ => base.NoContent(),
            Problem);
    }

    // GET: v1/Users/me
    [HttpGet("me")]
    [ActionName(nameof(GetMeAsync))]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetMeAsync(CancellationToken cancellationToken)
    {
        // Get the username from the current user's claims.
        var usernameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
        string email = usernameClaim?.Value ?? string.Empty;

        var query = new GetUserByEmailQuery(email);

        ErrorOr<UserResult> result = await Sender.Send(query, cancellationToken);

        return result.Match(
            userResult => base.Ok(Mapper.Map<UserWithRolesResponse>(userResult)),
            Problem);
    }
}
