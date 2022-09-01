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

    [HttpGet("dividends/{symbol}")]
    public async Task<IActionResult> GetDividends([FromRoute] string symbol, [FromQuery] DateTimeOffset? start, [FromQuery] DateTimeOffset? end)
        => Ok(await _queryDispatcher.QueryAsync(new GetDividends(symbol, start, end)));

    [HttpGet("search")]
    public async Task<IActionResult> SearchQuotes([FromQuery] SearchQuotes searchQuery)
        => Ok(await _queryDispatcher.QueryAsync(searchQuery));
}