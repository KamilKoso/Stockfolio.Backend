using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Stockfolio.Modules.Portfolios.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PortfoliosController : Controller
    {
        
    }
}
