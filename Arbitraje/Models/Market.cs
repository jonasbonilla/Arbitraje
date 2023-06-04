namespace Arbitraje.Models
{
    public class Market
    {
        public string key { get; set; }
        public DateTime last_update { get; set; }
        public List<Outcome> outcomes { get; set; }
    }
}
