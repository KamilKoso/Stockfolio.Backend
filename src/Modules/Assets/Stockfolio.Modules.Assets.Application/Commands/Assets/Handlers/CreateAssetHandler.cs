using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Modules.Assets.Core.Assets;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Contexts;
using Stockfolio.Shared.Core.Types;

namespace Stockfolio.Modules.Assets.Application.Commands.Assets.Handlers;

internal class CreateAssetHandler : ICommandHandler<CreateAsset>
{
    private readonly IAssetsRepository _assetsRepository;
    private readonly IContext _context;
    private readonly ILogger<CreateAssetHandler> _logger;

    public CreateAssetHandler(IAssetsRepository assetsRepository,
                              IContext context,
                              ILogger<CreateAssetHandler> logger)
    {
        _assetsRepository = assetsRepository;
        _context = context;
        _logger = logger;
    }

    public async Task HandleAsync(CreateAsset command, CancellationToken cancellationToken = default)
    {
        Asset asset = new(command.AssetName, UserId.New(_context.Identity.Id.Value), new(command.Currency));
        await _assetsRepository.CreateAsset(asset);
        _logger.LogInformation("Asset {AssetName} ({AssetId}) created by User with ID: {UserId}", command.AssetName, asset.Id, _context.Identity.Id);
    }
}