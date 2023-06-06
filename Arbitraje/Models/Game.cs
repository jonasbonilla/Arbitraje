using System.Diagnostics;

namespace Arbitraje.Models
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

    // arbitrage
    public class FootballEvent
    {
        public string Bookmaker { get; set; }
        public DateTime CommenceTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public decimal HomeOdds { get; set; }
        public decimal DrawOdds { get; set; }
        public decimal AwayOdds { get; set; }
    }

    // {'Bookmaker':'','HomeTeam':'', 'AwayTeam': '', 'HomeOdds':'', 'DrawOdds':'', 'AwayOdds':''  }

    //public class ArbitrageCalculator
    //{
    //    private List<FootballEvent> events;

    //    public ArbitrageCalculator()
    //    {
    //        events = new List<FootballEvent>();
    //    }

    //    public void AddFootballEvent(FootballEvent footballEvent)
    //    {
    //        events.Add(footballEvent);
    //    }

    //    public void PerformArbitrage()
    //    {
    //        foreach (var footballEvent in events)
    //        {
    //            var totalInverseOdds = 1 / footballEvent.HomeOdds + 1 / footballEvent.DrawOdds + 1 / footballEvent.AwayOdds;

    //            var homePercentage = (1 / footballEvent.HomeOdds) / totalInverseOdds;
    //            var drawPercentage = (1 / footballEvent.DrawOdds) / totalInverseOdds;
    //            var awayPercentage = (1 / footballEvent.AwayOdds) / totalInverseOdds;

    //            if (homePercentage < 1 || drawPercentage < 1 || awayPercentage < 1)
    //            {
    //                Debug.WriteLine("Arbitrage opportunity found!");

    //                // Calculate the stakes to bet on each outcome
    //                var totalStake = 100; // Total stake amount, you can change this value
    //                var homeStake = totalStake * homePercentage;
    //                var drawStake = totalStake * drawPercentage;
    //                var awayStake = totalStake * awayPercentage;

    //                Debug.WriteLine("Home Team: " + footballEvent.HomeTeam);
    //                Debug.WriteLine("Home Odds: " + footballEvent.HomeOdds);
    //                Debug.WriteLine("Stake on Home Team: " + homeStake);

    //                Debug.WriteLine("Draw");
    //                Debug.WriteLine("Draw Odds: " + footballEvent.DrawOdds);
    //                Debug.WriteLine("Stake on Draw: " + drawStake);

    //                Debug.WriteLine("Away Team: " + footballEvent.AwayTeam);
    //                Debug.WriteLine("Away Odds: " + footballEvent.AwayOdds);
    //                Debug.WriteLine("Stake on Away Team: " + awayStake);

    //                Debug.WriteLine("--------------------------------------------------");
    //            }
    //        }
    //    }
    //}

    public class ArbitrageOpportunityFinder
    {
        public FootballEvent FindBestOpportunity(List<FootballEvent> oddsList)
        {
            FootballEvent bestOpportunity = null;
            decimal bestArbitragePercentage = 0;

            foreach (var odds in oddsList)
            {
                decimal totalInverseOdds = 1 / odds.HomeOdds + 1 / odds.DrawOdds + 1 / odds.AwayOdds;
                decimal arbitragePercentage = 1 - totalInverseOdds;

                if (arbitragePercentage > bestArbitragePercentage)
                {
                    bestArbitragePercentage = arbitragePercentage;
                    bestOpportunity = odds;
                }
            }

            return bestOpportunity;
        }
    } 
}
