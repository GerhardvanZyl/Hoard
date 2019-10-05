using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RetrieveSteamGamesFn
{
    public static class RetrieveSteamGameInfo
    {

        private static HttpClient client = new HttpClient();
        private static Regex tags = new Regex("<a[^>]*class=\\\"app_tag\\\"[^>]*>([^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static string steamStoreUri = Environment.GetEnvironmentVariable("STEAM_STORE_GAMES_URI");
        private static string getOwnedGamesUri = Environment.GetEnvironmentVariable("STEAM_API_URI_GET_OWNED_GAMES");
        private static string key = Environment.GetEnvironmentVariable("STEAM_API_KEY");

        [FunctionName("RetrieveSteamGameInfo")]
        public static async Task Run([QueueTrigger("steam-users", Connection = "QUEUE_CONNECTION")]string user, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {user}");

            HttpResponseMessage gamesHttpResponse = await client.GetAsync(getOwnedGamesUri + $"&steamid={steamId}" + $"&key={key}");
            string temp = await gamesHttpResponse.Content.ReadAsStringAsync();
            SteamGamesResponseContainer gamesResponse = await gamesHttpResponse.Content.ReadAsAsync<SteamGamesResponseContainer>();

            // TODO: place holder to limit to 10 for now. Need to move to an async operation
            int i = 0;
            foreach (Game game in gamesResponse.Response.Games)
            {
                i++;
                HttpResponseMessage gamePageResponse = await client.GetAsync(steamStoreUri + game.AppId);
                string gamesPage = await gamePageResponse.Content.ReadAsStringAsync();

                SetTags(game, gamesPage);

                if (i > 10) break;
            }
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
