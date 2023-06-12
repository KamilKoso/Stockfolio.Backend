using Stockfolio.Modules.Assets.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Core.ValueObjects;
internal record AssetName
{
    private static readonly int _maximumLength = 40;
    public string Value { get; init; }
    public object Length { get; internal set; }

    public AssetName(string value)
    {
        if (value.Length >= _maximumLength)
        {
            throw new AssetNameTooLongException(value, _maximumLength);
        }
        Value = value;
    }

    public static implicit operator string(AssetName asset) => asset.Value;

    public static implicit operator AssetName(string asset) => new AssetName(asset);
    public override string ToString() => Value;
}