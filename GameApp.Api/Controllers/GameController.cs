using GameApp.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("getgamelist")]
        public async Task<IActionResult> GetGameList()
        {
            var data = await _gameService.GetGames();
            return Ok(data);
        }
    }
}
