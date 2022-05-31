using System;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure.Postgres;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}