using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Modules.Assets.Core;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Contexts;

namespace Stockfolio.Modules.Assets.Application.Commands.Assets.Handlers;

internal class CreateAssetHandler : ICommandHandler<CreateAsset>
{
    private readonly IAssetsRepository _assetsRepository;
    private readonly IIdentityContext _identityContext;
    private readonly ILogger<CreateAssetHandler> _logger;

    public CreateAssetHandler(IAssetsRepository assetsRepository,
                              IIdentityContext identityContext,
                              ILogger<CreateAssetHandler> logger)
    {
        _assetsRepository = assetsRepository;
        _identityContext = identityContext;
        _logger = logger;
    }

    public async Task HandleAsync(CreateAsset command, CancellationToken cancellationToken = default)
    {
        Asset asset = new(command.AssetName, _identityContext.Id, new(command.Currency));
        await _assetsRepository.CreateAsset(asset);
        _logger.LogInformation("Asset {AssetName} ({AssetId}) created by User with ID: {UserId}", command.AssetName, asset.Id, _identityContext.Id);
    }
}