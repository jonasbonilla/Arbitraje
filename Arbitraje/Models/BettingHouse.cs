﻿namespace Arbitraje.Models
{
    public  class BettingHouse
    {
        public string region_key { get; set; }
        public string bookmaker_key { get; set; }
        public string bookmaker { get; set; }
        public string DisplayName => $"{bookmaker} ({region_key.ToUpper()})";
    }
}
