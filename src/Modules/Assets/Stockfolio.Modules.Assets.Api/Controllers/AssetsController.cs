using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.Assets.Application.Queries.Assets;
using Stockfolio.Shared.Abstractions.Queries;
using System;
using System.Threading.Tasks;

namespace Stockfolio.Modules.Assets.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class AssetsController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public AssetsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssets([FromQuery] GetAssets query)
        => Ok(await _queryDispatcher.QueryAsync(query));

    [HttpGet("historical/{symbol}")]
    public async Task<IActionResult> GetHistoricalData([FromRoute] string symbol, [FromQuery] DateTimeOffset? start, [FromQuery] DateTimeOffset? end, [FromQuery] string interval, [FromQuery] string range)
        => Ok(await _queryDispatcher.QueryAsync(new GetHistoricalQuotes(symbol, start, end, range, interval)));

    [HttpGet("search")]
    public async Task<IActionResult> SearchAssets([FromQuery] SearchAssets query)
        => Ok(await _queryDispatcher.QueryAsync(query));
}