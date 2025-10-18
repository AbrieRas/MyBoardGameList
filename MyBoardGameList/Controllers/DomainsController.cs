using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyBoardGameList.DTO;
using MyBoardGameList.Models;

namespace MyBoardGameList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DomainsController> _logger;

        public DomainsController(
            ApplicationDbContext context,
            ILogger<DomainsController> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<Domain[]>> Get()
        {

        }
    }
}
