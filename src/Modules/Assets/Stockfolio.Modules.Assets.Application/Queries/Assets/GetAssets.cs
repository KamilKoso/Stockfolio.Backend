using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets;

internal class GetAssets : IQuery<IEnumerable<AssetDto>>
{
    public IEnumerable<string> Symbols { get; set; }
}