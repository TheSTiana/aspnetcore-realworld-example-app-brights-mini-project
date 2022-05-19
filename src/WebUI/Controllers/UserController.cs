using Application.Features.Auth.Commands;
using Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Users;

[Route("[controller]")]
[ApiExplorerSettings(GroupName = "User and Authentication")]
public class UserController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get current user
    /// </summary>
    /// <remarks>Gets the currently logged-in user</remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetCurrentUser")]
    [Authorize]
    public Task<UserResponse> Current(CancellationToken cancellationToken)
        => _mediator.Send(new CurrentUserQuery(), cancellationToken);

    /// <summary>
    /// Update current user
    /// </summary>
    /// <remarks>Updated user information for current user</remarks>
    /// <param name="command">User details to update. At least <strong>one</strong> field is required.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut(Name = "UpdateCurrentUser")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public Task<UserResponse> Update([FromBody] UpdateUserRequest command, CancellationToken cancellationToken)
        => _mediator.Send(command, cancellationToken);
}