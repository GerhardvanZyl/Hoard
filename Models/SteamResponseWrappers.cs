using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hoard.Model
{
    public class SteamGamesResponse
    {
        [JsonProperty("game_count")]
        public int GameCount { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }

    public class SteamGamesResponseContainer
    {
        [JsonProperty("response")]
        public SteamGamesResponse Response {get; set;}
    }

}