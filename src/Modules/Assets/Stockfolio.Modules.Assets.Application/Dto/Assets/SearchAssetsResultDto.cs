namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record SearchAssetsResultDto
{
    public SearchAssetsResultDto(IList<SearchSecurityDto> quotes)
    {
        Quotes = (quotes ?? Array.Empty<SearchSecurityDto>()).AsReadOnly();
    }

    public IReadOnlyCollection<SearchSecurityDto> Quotes { get; }
    public int Count { get => Quotes.Count; }
}