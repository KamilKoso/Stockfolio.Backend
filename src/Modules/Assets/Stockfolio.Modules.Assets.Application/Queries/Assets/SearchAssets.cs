using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets;

internal record SearchAssets : IQuery<SearchAssetsResultDto>
{
    private int _count = 7;

    public string Query { get; }

    public int Count
    {
        get => _count;
        set => _count = value switch
        {
            <= 0 => 6,
            _ => value
        };
    }
}