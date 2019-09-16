using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hoard.Model;

namespace Hoard.Services
{
    public class GameRepository
    {
        private static HttpClient client = new HttpClient();
        private Regex tags = new Regex("<a[^>]*class=\\\"app_tag\\\"[^>]*>([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static string steamStoreUri = Environment.GetEnvironmentVariable("STEAM_STORE_URI");
        private static string getOwnedGamesUri = Environment.GetEnvironmentVariable("STEAM_API_URI_GET_OWNED_GAMES");
        private static string key = Environment.GetEnvironmentVariable("STEAM_STORE_URI");

        public async Task<List<Game>> GetGamesFor(string steamId)
        {
            HttpResponseMessage gamesHttpResponse = await client.GetAsync(getOwnedGamesUri + $"&steamid={steamId}" + $"&key={key}");
            SteamGamesResponseContainer gamesResponse = await gamesHttpResponse.Content.ReadAsAsync<SteamGamesResponseContainer>();

            // TODO: place holder to limit to 10 for now. Need to move to an async operation
            int i = 0;
            foreach (Game game in gamesResponse.Response.Games)
            {
                i++;
                HttpResponseMessage gameResponse = await client.GetAsync(steamStoreUri + game.AppId);
                string gamesPage = await gameResponse.Content.ReadAsStringAsync();

                SetTags(game, gamesPage);

                if (i > 10) break;
            }

            return gamesResponse.Response.Games;
        }

        private void SetTags(Game game, string pageHtml)
        {
            MatchCollection matches = tags.Matches(pageHtml);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    string tag = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(tag))
                    {
                        game.Tags.Add(tag);
                    }
                }
            }
        }

    }
}