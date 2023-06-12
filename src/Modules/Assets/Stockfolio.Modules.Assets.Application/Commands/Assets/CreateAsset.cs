using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Assets.Application.Commands.Assets;
internal record CreateAsset(string AssetName, string Currency) : ICommand;