using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.StockMarket.Application.Queries;
using Stockfolio.Shared.Abstractions.Queries;
using System;
using System.Threading.Tasks;

namespace Stockfolio.Modules.StockMarket.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class QuotesController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public QuotesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuotes([FromQuery] GetQuotes query)
        => Ok(await _queryDispatcher.QueryAsync(query));

    [HttpGet("historical-data/{symbol}")]
    public async Task<IActionResult> GetHistoricalData([FromRoute] string symbol, [FromQuery] DateTimeOffset? start, [FromQuery] DateTimeOffset? end, [FromQuery] string interval, [FromQuery] string range)
        => Ok(await _queryDispatcher.QueryAsync(new GetHistoricalData(symbol, start, end, range, interval)));

    [HttpGet("search")]
    public async Task<IActionResult> SearchQuotes([FromQuery] SearchQuotes searchQuery)
        => Ok(await _queryDispatcher.QueryAsync(searchQuery));
}