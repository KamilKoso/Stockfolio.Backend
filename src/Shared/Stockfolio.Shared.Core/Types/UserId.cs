using Stockfolio.Shared.Core.Identity;

namespace Stockfolio.Shared.Core.Types;

public record UserId : StockfolioId
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId New(Guid id) => new(id);
};