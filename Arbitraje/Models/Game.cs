﻿namespace Arbitraje.Models
{
    public class Game
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public List<Bookmaker> bookmakers { get; set; }
    }
}
