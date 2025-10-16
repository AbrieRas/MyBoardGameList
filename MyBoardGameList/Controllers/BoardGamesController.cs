using Microsoft.AspNetCore.Mvc;
using MyBoardGameList.DTO;
using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Models;
using System.Linq.Dynamic.Core;

namespace MyBoardGameList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(
            ApplicationDbContext context,
            ILogger<BoardGamesController> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<BoardGame[]>> Get(
            int pageIndex = 0,
            int pageSize = 10,
            string? sortColumn = "Name",
            string? sortOrder = "ASC",
            string? filterQuery = null
        )
        {
            // Handle DbSet as an IQueryable object
            var query = _context.BoardGames.AsQueryable();

            // Conditionally apply filtering
            if (!string.IsNullOrEmpty(filterQuery))
                query = query.Where(b => b.Name.StartsWith(filterQuery));

            // Determine record count before pagination
            var recordCount = await query.CountAsync();

            // Apply sorting and pagination
            query = query
                .OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = recordCount,
                Links = new List<LinkDTO> {
                        new LinkDTO(
                            Url.Action(
                                null,
                                "BoardGames",
                                new { pageIndex, pageSize },
                                Request.Scheme
                            )!,
                            "self",
                            "GET"
                        ),
                    }
            };
        }

        [HttpPost(Name = "UpdateBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Post(BoardGameDTO model)
        {
            var boardGame = await _context.BoardGames
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();

            if (boardGame != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                    boardGame.Name = model.Name;
                if (model.Year.HasValue && model.Year.Value > 0)
                    boardGame.Year = model.Year.Value;
                if (model.MinPlayers.HasValue && model.MinPlayers.Value > 0)
                    boardGame.MinPlayers = model.MinPlayers.Value;
                if (model.MaxPlayers.HasValue && model.MaxPlayers.Value > 0)
                    boardGame.MaxPlayers = model.MaxPlayers.Value;
                if (model.PlayTime.HasValue && model.PlayTime.Value > 0)
                    boardGame.PlayTime = model.PlayTime.Value;
                if (model.MinAge.HasValue && model.MinAge.Value > 0)
                    boardGame.MinAge = model.MinAge.Value;
                boardGame.LastModifiedDate = DateTime.Now;
                _context.BoardGames.Update(boardGame);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<BoardGame?>()
            {
                Data = boardGame,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "BoardGames",
                            model,
                            Request.Scheme)!,
                        "self",
                        "POST"
                    ),
                }
            };
        }

        [HttpDelete(Name = "DeleteBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Delete(string ids)
        {
            var idArray = ids.Split(',').Select(x => int.Parse(x));
            var deleteBoardGameList = new List<BoardGame>();

            foreach (int id in idArray)
            {
                var boardGame = await _context.BoardGames
                    .Where(b => b.Id == id)
                    .FirstOrDefaultAsync();
                if (boardGame != null)
                {
                    deleteBoardGameList.Add(boardGame);
                    _context.BoardGames.Remove(boardGame);
                    await _context.SaveChangesAsync();
                };
            }

            return new RestDTO<BoardGame[]?>()
            {
                Data = deleteBoardGameList.Count > 0 ? deleteBoardGameList.ToArray() : null,
                Links = new List<LinkDTO>
                    {
                        new LinkDTO(
                            Url.Action(
                                null,
                                "BoardGames",
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                    }
            };
        }
    }
}
