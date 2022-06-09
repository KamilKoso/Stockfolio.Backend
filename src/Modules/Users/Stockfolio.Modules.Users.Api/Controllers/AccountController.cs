using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.Users.Core.Commands;
using Stockfolio.Modules.Users.Core.DTO;
using Stockfolio.Modules.Users.Core.Queries;
using Stockfolio.Modules.Users.Core.Services;
using Stockfolio.Shared.Abstractions.Contexts;
using Stockfolio.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;

namespace Stockfolio.Modules.Users.Api.Controllers;

internal class AccountController : BaseController
{
    private const string AccessTokenCookie = "__access-token";
    private readonly IDispatcher _dispatcher;
    private readonly IContext _context;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly CookieOptions _cookieOptions;

    public AccountController(IDispatcher dispatcher,
                             IContext context,
                             IUserRequestStorage userRequestStorage,
                             CookieOptions cookieOptions)
    {
        _dispatcher = dispatcher;
        _context = context;
        _userRequestStorage = userRequestStorage;
        _cookieOptions = cookieOptions;
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Get account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDetailsDto>> GetAsync()
        => OkOrNotFound(await _dispatcher.QueryAsync(new GetUser { UserId = _context.Identity.Id }));

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignIn command)
    {
        await _dispatcher.SendAsync(command);
        var jwt = _userRequestStorage.GetToken(command.Id);
        var user = await _dispatcher.QueryAsync(new GetUser { UserId = jwt.UserId });
        AddCookie(AccessTokenCookie, jwt.AccessToken);
        return Ok(user);
    }

    [HttpPost("generate-email-confirmation-token")]
    [SwaggerOperation("Generate email confirmation token")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GenerateEmailConfirmationTokenAsync(GenerateEmailConfirmationToken command)
    {
        await _dispatcher.SendAsync(command);
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

    [Authorize]
    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SignOutAsync()
    {
        await _dispatcher.SendAsync(new SignOut(_context.Identity.Id));
        DeleteCookie(AccessTokenCookie);
        return NoContent();
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}