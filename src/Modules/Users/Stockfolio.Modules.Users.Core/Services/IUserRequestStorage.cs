using Stockfolio.Shared.Abstractions.Auth;

namespace Stockfolio.Modules.Users.Core.Services;

internal interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);

    JsonWebToken GetToken(Guid commandId);
}