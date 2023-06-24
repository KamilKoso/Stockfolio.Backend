using Stockfolio.Modules.Assets.Core.Assets.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets;
internal record AssetName
{
    public static readonly int MaximumLength = 40;
    public string Value { get; init; }
    public object Length { get; internal set; }

    public AssetName(string value)
    {
        if (value.Length >= MaximumLength)
        {
            throw new AssetNameTooLongException(value, MaximumLength);
        }
        Value = value;
    }

    public static implicit operator string(AssetName asset) => asset.Value;

    public static implicit operator AssetName(string asset) => new AssetName(asset);
    public override string ToString() => Value;
}