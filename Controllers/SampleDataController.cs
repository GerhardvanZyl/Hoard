using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hoard.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<string> All()
        {
            return new string[] {"a", "b"};
        }
    }
}
