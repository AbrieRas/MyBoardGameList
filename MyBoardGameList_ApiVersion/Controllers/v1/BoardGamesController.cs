using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyBoardGameList.DTO.v1;

namespace MyBoardGameList.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;
        private readonly bool isStaging = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging";

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public RestDTO<BoardGame[]> Get()
        {
            if (isStaging)
            {
                return new RestDTO<BoardGame[]>()
                {
                    Data = new BoardGame[] {
                        new BoardGame() {
                            Id = 1,
                            Name = "Axis & Allies",
                            Year = 1981,
                            MinPlayers = 2,
                            MaxPlayers = 5
                        },
                        new BoardGame() {
                            Id = 2,
                            Name = "Citadels",
                            Year = 2000,
                            MinPlayers = 2,
                            MaxPlayers = 8
                        },
                        new BoardGame() {
                            Id = 3,
                            Name = "Terraforming Mars",
                            Year = 2016,
                            MinPlayers = 1,
                            MaxPlayers = 5
                        }
                    },
                    Links = new List<LinkDTO> {
                        new LinkDTO(
                            Url.Action(null, "BoardGames", null, Request.Scheme)!,
                            "self",
                            "GET"
                        ),
                    }
                };
            }
            else
            {
                return new RestDTO<BoardGame[]>()
                {
                    Data = new BoardGame[] {
                        new BoardGame() {
                            Id = 1,
                            Name = "Axis & Allies",
                            Year = 1981,
                        },
                        new BoardGame() {
                            Id = 2,
                            Name = "Citadels",
                            Year = 2000,
                        },
                        new BoardGame() {
                            Id = 3,
                            Name = "Terraforming Mars",
                            Year = 2016,
                        }
                    },
                    Links = new List<LinkDTO> {
                        new LinkDTO(
                            Url.Action(null, "BoardGames", null, Request.Scheme)!,
                            "self",
                            "GET"
                        ),
                    }
                };
            }
        }
    }
}
