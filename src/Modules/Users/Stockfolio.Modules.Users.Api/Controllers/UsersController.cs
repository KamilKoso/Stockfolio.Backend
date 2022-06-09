using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Queries;
using Stockfolio.Shared.Abstractions.Dispatchers;
using Stockfolio.Shared.Abstractions.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Stockfolio.Modules.Users.Api.Controllers;

[Authorize(Policy)]
internal class UsersController : BaseController
{
    private const string Policy = "users";
    private readonly IDispatcher _dispatcher;

    public UsersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{userId:guid}")]
    [SwaggerOperation("Get user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync(Guid userId)
        => OkOrNotFound(await _dispatcher.QueryAsync(new GetUser { UserId = userId }));

    [HttpGet]
    [SwaggerOperation("Browse users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Paged<UserDto>>> BrowseAsync([FromQuery] BrowseUsers query)
        => Ok(await _dispatcher.QueryAsync(query));
}