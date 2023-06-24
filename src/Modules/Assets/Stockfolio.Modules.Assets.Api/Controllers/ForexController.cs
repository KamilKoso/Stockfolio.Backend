using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.Assets.Application.Queries.Forex;
using Stockfolio.Shared.Abstractions.Queries;
using System.Threading.Tasks;

namespace Stockfolio.Modules.Portfolios.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class ForexController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public ForexController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("supported-currencies")]
    public async Task<IActionResult> SupportedCurrencies()
        => Ok(await _queryDispatcher.QueryAsync(new GetSupportedCurrencies()));

    [HttpGet("exchange-rates")]
    public async Task<IActionResult> GetExchangeRates([FromQuery] GetExchangeRates query)
        => Ok(await _queryDispatcher.QueryAsync(query));
}