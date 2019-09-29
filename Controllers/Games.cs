using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoard.Model;
using Hoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hoard.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private GameRepository _gamesRepository;

        public GamesController(GameRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Game>> All(string steamId)
        {
            List<Game> games = await _gamesRepository.GetGamesFor(steamId);


            return games;
        }
    }
}
