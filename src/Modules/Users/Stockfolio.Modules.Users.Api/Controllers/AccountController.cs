using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.Users.Core.Commands;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Queries;
using Stockfolio.Shared.Abstractions.Contexts;
using Stockfolio.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace Stockfolio.Modules.Users.Api.Controllers;

[Authorize]
internal class AccountController : BaseController
{
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;

    public AccountController(IDispatcher dispatcher,
                             IContext context)
    {
        _dispatcher = dispatcher;
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation("Get account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetAsync()
        => OkOrNotFound(await _dispatcher.QueryAsync(new GetUser { UserId = _context.Identity.Id }));

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> SignInAsync(SignIn command)
    {
        await _dispatcher.SendAsync(command);
        var user = await _dispatcher.QueryAsync(new GetUser { UserId = _context.Identity.Id });
        return Ok(user);
    }

    [HttpPost("generate-email-confirmation-token")]
    [SwaggerOperation("Generate email confirmation token")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateEmailConfirmationTokenAsync()
    {
        await _dispatcher.SendAsync(new GenerateEmailConfirmationToken(_context.Identity.Id));
        return NoContent();
    }

    [HttpPost("confirm-email")]
    [SwaggerOperation("Confirm email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConfirmEmail(ConfirmEmail command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SignOutAsync()
    {
        await _dispatcher.SendAsync(new SignOut(_context.Identity.Id));
        return NoContent();
    }
}