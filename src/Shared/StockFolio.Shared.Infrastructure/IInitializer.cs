using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure;

public interface IInitializer
{
    Task InitAsync();
}