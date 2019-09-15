using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hoard.Model
{

    public class Game
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("has_community_visible_stats")]
        public bool HasCommunityVisibleStats { get; set; }

        [JsonProperty("img_icon_url")]
        public string ImgIconUrl { get; set; }

        [JsonProperty("img_logo_url")]
        public string ImgLogoUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playtime_forever")]
        public int PlaytimeForever { get; set; }

        [JsonProperty("playtime_linux_forever")]
        public int PlaytimeLinuxForever { get; set; }

        [JsonProperty("playtime_mac_forever")]
        public int PlaytimeMacForever { get; set; }

        [JsonProperty("playtime_windows_forever")]
        public int PlaytimeWindowsForever { get; set; }

        public List<string> Tags {get;set;} = new List<string>();
    }

}

