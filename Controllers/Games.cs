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
        [HttpGet("[action]")]
        public async Task<IEnumerable<Game>> All(string steamId)
        {
            GameRepository repo = new GameRepository();
            List<Game> games = await repo.GetGamesFor(steamId);


            return games;
        }
    }
}
