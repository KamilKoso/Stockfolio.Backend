namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record SearchSecurityDto : SecurityBaseDto
{
    public string ExchangeDisplayName { get; init; }
    public string Industry { get; init; }
    public string Sector { get; init; }
}