using GameApp.Model.Dtos;
using GameApp.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Api.Controllers
{
    //[Authorize(Roles = "admin")]
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
        public async Task<IActionResult> GetGameListAsync()
        {
            var data = await _gameService.GetGamesAsync();
            return Ok(data);
        }

        [HttpGet("getgamedetail/{id}")]
        public async Task<IActionResult> GetGameAsync([FromRoute] int id)
        {
            var data = await _gameService.GetGameDetailAsync(id);
            return Ok(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGameAsync([FromBody] GameFormDto gameFormDto)
        {
            var data = await _gameService.AddGameAsync(gameFormDto);
            return Ok(data);
        }
    }
}
