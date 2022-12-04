using System;

namespace NagagQuizWebv2.Models
{
    public class LiveMatchModel
    {
        public int event_key { get; set; }
        public DateTime event_date { get; set; }
        public string event_home_team { get; set; }
        public int home_team_key { get; set; }
        public string event_away_team { get; set; }
        public int away_team_key { get; set; }
        public string event_final_result { get; set; }
        public string home_team_logo { get; set; }
        public string away_team_logo { get; set; }
        public string event_status { get; set; }
        public string league_name { get; set; }
        public string country_name { get; set; }
    }
}
