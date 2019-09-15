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

        // TODO: Move these to environment storage
        private static string steamStoreUri = "https://store.steampowered.com/app/";
        private static string getOwnedGames = "GetOwnedGames";
        private static string version = "v0001";

        private static string key = "";
        
        private static string steamApiUri = $"http://api.steampowered.com/IPlayerService/{getOwnedGames}/{version}/?key={key}&include_appinfo={true}&format=json";
        
        async Task<List<Game>> GetGamesFor(string steamId)
        {
            HttpResponseMessage response = await client.GetAsync(steamApiUri + $"&steamid={steamId}");

            SteamGamesResponseContainer returnValue = await response.Content.ReadAsAsync<SteamGamesResponseContainer>();
            string str = await response.Content.ReadAsStringAsync();

            // TODO: place holder to limit to 10 for now. Need to move to an async operation
            int i = 0;
            foreach (Game game in returnValue.Response.Games)
            {
                i++;
                HttpResponseMessage gameResponse = await client.GetAsync(steamStoreUri + game.AppId);
                string gamesPage = await gameResponse.Content.ReadAsStringAsync();

                Regex re = new Regex("<a[^>]*class=\\\"app_tag\\\"[^>]*>([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                MatchCollection matches = re.Matches(gamesPage);
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

                if (i > 10) break;
            }

            return returnValue.Response.Games;
        }
    }

}