using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stockfolio.Modules.StockMarket.Application.Queries;
using Stockfolio.Shared.Abstractions.Queries;
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
    {
        var result = await _queryDispatcher.QueryAsync(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchQuotes([FromQuery] SearchQuotes searchQuery)
    {
        var result = await _queryDispatcher.QueryAsync(searchQuery);
        return Ok(result);
    }
}