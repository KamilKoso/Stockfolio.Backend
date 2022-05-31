namespace Stockfolio.Modules.Users.Core.Options;

internal class RegistrationOptions
{
    public bool Enabled { get; init; }
    public IEnumerable<string> InvalidEmailProviders { get; init; }
}