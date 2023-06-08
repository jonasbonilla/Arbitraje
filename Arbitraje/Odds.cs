using Arbitraje.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Arbitraje
{
    public partial class Odds : Form
    {
        string apiKey = "3f415e1e00c15eea72cf0cf501ac225a";
        string baseUrl = "https://api.the-odds-api.com/v4";
        HttpClient _client;

        List<Sport> _listSports;
        List<BettingHouse> _listBettingHouse;
        List<BettingMarkets> _listBettingMarkets;
        List<Game> _listGames;

        public Odds()
        {
            InitializeComponent();

            _client = new HttpClient();

            _listSports = new List<Sport>();
            _listBettingHouse = new List<BettingHouse>();
            _listBettingMarkets = new List<BettingMarkets>();
            _listGames = new List<Game>();

            // fecha
            var hoy = DateTime.Now;
            dtpDateFrom.Value = new DateTime(hoy.Year, hoy.Month, hoy.Day, 0, 0, 0);
            dtpDateTo.Value = new DateTime(hoy.Year, hoy.Month, hoy.Day + 1, 0, 0, 0);
            pnlLoading.BringToFront();
        }

        private async Task Inicializar()
        {
            // obtener casas de apuesta
            _listBettingHouse = await GetBettingHouses();
            cbxBookmarkers.DataSource = _listBettingHouse.OrderBy(x => x.bookmaker).ThenBy(y => y.region_key).ToList();
            cbxBookmarkers.ValueMember = nameof(BettingHouse.bookmaker_key);
            cbxBookmarkers.DisplayMember = nameof(BettingHouse.DisplayName);

            // obtener marcadores
            _listBettingMarkets = await GetBettingMarkets();
            cbxBettingMarket.DataSource = _listBettingMarkets.OrderBy(x => x.market_name).ToList();
            cbxBettingMarket.ValueMember = nameof(BettingMarkets.market_key);
            cbxBettingMarket.DisplayMember = nameof(BettingMarkets.market_name);

            // Obtener deportes
            _listSports = await GetSports(baseUrl, apiKey);
            if (_listSports.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de deportes");
                return;
            }

            // grupos
            var grupos = _listSports.GroupBy(x => x.group).ToList();
            cbxGroup.DataSource = grupos;
            cbxGroup.ValueMember = nameof(Sport.key);
            cbxGroup.DisplayMember = nameof(Sport.description);

            //// chcbxGroup.Items.Add(new CheckComboBox.CheckComboBoxItem(grp.description, false));
            //foreach (var grp in grupos.GetEnumerator())
            //{
            //    Debug.WriteLine(grp.description);
            //}

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            pnlLoading.Visible = true;
            await Inicializar();
            pnlLoading.Visible = false;
        }

        private void cbxGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            // deportes
            if (cbxGroup.SelectedValue == null) return;
            cbxSports.DataSource = _listSports.OrderBy(x => x.description).ToList().FindAll(x => x.group.ToUpper().Equals(cbxGroup.SelectedValue.ToString().ToUpper()));
            cbxSports.ValueMember = nameof(Sport.key);
            cbxSports.DisplayMember = nameof(Sport.description);
        }

        // this message handler gets called when the user checks/unchecks an item the combo box
        //private void checkComboBox1_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (sender is CheckComboBox.CheckComboBoxItem)
        //    {
        //        CheckComboBox.CheckComboBoxItem item = (CheckComboBox.CheckComboBoxItem)sender;
        //        switch (item.Text)
        //        {
        //            case "One":
        //                checkBox1.Checked = item.CheckState;
        //                break;
        //            case "Two":
        //                checkBox2.Checked = item.CheckState;
        //                break;
        //            case "Three":
        //                checkBox3.Checked = item.CheckState;
        //                break;
        //        }
        //    }
        //}




















        private async void button1_Click(object sender, EventArgs e)
        {
            if (cbxGroup.SelectedValue == null || cbxBookmarkers.SelectedValue == null || cbxBettingMarket.SelectedValue == null)
            {
                MessageBox.Show("Error al cargar datos");
                return;
            }

            // Ejemplo de uso del método GetOdds con valores personalizados
            string sport = cbxSports.SelectedValue.ToString(); // UN SOLO GRUPO, UNA SOLA CATEGORIA
            string regions = _listBettingHouse.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString()).region_key; // eu - se puede concatenar
            string markets = cbxBettingMarket.SelectedValue.ToString(); // H2H - se puede concatenar

            // consultamos juegos
            txtResponse.Clear();
            _listGames = await GetGames(baseUrl, apiKey, sport, regions, markets);
            if (_listGames.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de juegos");
                return;
            }

            // filtramos games by bookmarket - obtiene todos los juegos cuyo bookmaker sea "xxx"

            var selectedBookmaker = _listBettingHouse.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString());
            var gamesFromBookmaker = _listGames
                .Select(game => new Game
                {
                    id = game.id,
                    sport_key = game.sport_key,
                    sport_title = game.sport_title,
                    commence_time = game.commence_time,
                    home_team = game.home_team,
                    away_team = game.away_team,
                    bookmakers = game.bookmakers
                        .Where(bookmaker => bookmaker.key == selectedBookmaker.bookmaker_key || bookmaker.key == "betfair" || bookmaker.key == "betsson")
                        .ToList()
                })
                .Where(game => game.bookmakers.Any() && (game.commence_time >= dtpDateFrom.Value && game.commence_time <= dtpDateTo.Value))
                .ToList();

            // validaciones
            if (gamesFromBookmaker.Count == 0)
            {
                txtResponse.AppendText("No se encontraron eventos para las opciones seleccionadas");
                return;
            }


            //  Mostrar los juegos filtrados (todo)
            var eventos = new List<FootballEvent>();
            foreach (Game game in gamesFromBookmaker)
            {
                foreach (Bookmaker bookmaker in game.bookmakers)
                {
                    eventos.Add(new FootballEvent
                    {
                        Bookmaker = bookmaker.key,
                        CommenceTime = game.commence_time,
                        HomeTeam = game.home_team.Trim(),
                        AwayTeam = game.away_team.Trim(),
                        HomeOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == game.home_team.Trim()).price,
                        AwayOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == game.away_team.Trim()).price,
                        //DrawOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == "Draw").price,
                    });
                }
            }


            // presento eventos
            // Create an instance of the ArbitrageCalculator class
            foreach (FootballEvent evento in eventos)
            {
                txtResponse.AppendText($"{evento.Bookmaker.ToUpper()} - {evento.CommenceTime.ToLongDateString()} / {evento.CommenceTime.ToLongTimeString()} \n");
                txtResponse.AppendText($"{evento.HomeTeam} vs. {evento.AwayTeam}\n");
                txtResponse.AppendText($"HomeOdds: {evento.HomeOdds}\n");
                //txtResponse.AppendText($"DrawOdds: {evento.DrawOdds}\n");
                txtResponse.AppendText($"AwayOdds: {evento.AwayOdds}\n");
                txtResponse.AppendText("\n\n");
            }

            // Arbitraje
            // Cálculo de arbitraje para una lista de apuestas del mismo evento pero de diferentes bookmakers.
            // El programa presenta en pantalla la mejor oportunidad, la cantidad a invertir en cada probabilidad y la ganancia esperada.
            // Se asume un capital inicial de $100.

            //pnlLoading.Visible = true;
            //var resultp = await ShowProbabilities(eventos);
            //txtResponse.AppendText(resultp);

            //txtResponse.AppendText("\n\n");
            //txtResponse.AppendText("\n\n");


            //// arbitraje para la misma casa
            //var arbitrageResults1 = await CalculateArbitrage1(eventos, 100m);
            //foreach (var result in arbitrageResults1) txtResponse.AppendText(result);

            //txtResponse.AppendText("\n\n");
            //txtResponse.AppendText("\n\n");

            //// arbitraje para la diferentes casas
            //var arbitrageResults2 = await CalculateArbitrageX(eventos, 100m);
            //foreach (var result in arbitrageResults2) txtResponse.AppendText(result);

            //pnlLoading.Visible = false;
        }

        private async Task<List<BettingHouse>> GetBettingHouses()
        {
            // For updates - https://the-odds-api.com/sports-odds-data/bookmaker-apis.html
            List<BettingHouse> result;
            try
            {
                string jsonText = File.ReadAllText("betting_houses.txt");
                result = JsonConvert.DeserializeObject<List<BettingHouse>>(jsonText);
            }
            catch (Exception ex)
            {
                result = new List<BettingHouse>();
                Console.WriteLine("Error on GetBettingHouses: " + ex.Message);
            }
            return result;
        }

        private async Task<List<BettingMarkets>> GetBettingMarkets()
        {
            // For updates - https://the-odds-api.com/sports-odds-data/betting-markets.html
            List<BettingMarkets> result;
            try
            {
                string jsonText = File.ReadAllText("betting_markets.txt");
                result = JsonConvert.DeserializeObject<List<BettingMarkets>>(jsonText);
            }
            catch (Exception ex)
            {
                result = new List<BettingMarkets>();
                Console.WriteLine("Error on GetBettingMarkets: " + ex.Message);
            }
            return result;
        }

        private async Task<List<Sport>> GetSports(string baseUrl, string ApiKey, bool fromApi = false)
        {
            List<Sport> result;
            try
            {
                string jsonText;
                if (fromApi)
                {
                    // Por API
                    var url = $"{baseUrl}/sports/?apiKey={ApiKey}";
                    var response = await _client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    jsonText = await response.Content.ReadAsStringAsync();

                    // Escribir al TXT 
                    File.WriteAllText("sports.txt", jsonText);
                }
                else
                {
                    // Leer del TXT
                    jsonText = File.ReadAllText("sports.txt");
                }

                result = JsonConvert.DeserializeObject<List<Sport>>(jsonText);
            }
            catch (Exception ex)
            {
                result = new List<Sport>();
                Console.WriteLine("Error on GetSports: " + ex.Message);
            }
            return result;
        }















        private async Task<List<Game>> GetGames(string baseUrl, string apiKey, string sport, string regions, string markets, bool fromApi = false)
        {
            List<Game> result;
            try
            {
                string jsonText;
                if (fromApi)
                {
                    // Por API
                    var url = $"{baseUrl}/sports/{sport}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
                    var response = await _client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    jsonText = await response.Content.ReadAsStringAsync();

                    // Escribir al TXT 
                    File.WriteAllText($"{sport}.txt", jsonText);
                }
                else
                {
                    // Leer del TXT 
                    jsonText = File.ReadAllText($"{sport}.txt");
                }
                result = JsonConvert.DeserializeObject<List<Game>>(jsonText);
            }
            catch (Exception ex)
            {
                result = new List<Game>();
                Console.WriteLine("Error on GetGames: " + ex.Message);
            }
            return result;
        }



















        private async Task<string> ShowProbabilities(List<FootballEvent> bets)
        {
            var response = $"\nProbabilidades calculadas ({cbxBettingMarket.SelectedValue.ToString()}): --------------------------------\n";
            foreach (var bet in bets)
            {
                //decimal probability = 1 / (1 + bet.HomeOdds / bet.AwayOdds);
                //response += $"Bookmaker: {bet.Bookmaker}\n";
                //response += $"Probabilidad: {probability:P2}\n";

            }
            return response;
        }

        public async Task<List<string>> CalculateArbitrage1(List<FootballEvent> bets, decimal amount)
        {
            List<string> results = new List<string>();

            foreach (var bet in bets)
            {
                //var totalInverseOdds = (1 / bet.HomeOdds) + (1 / bet.AwayOdds);
                var totalInverseOdds = 100;

                // porcentaje
                var homePercentage = 1 / (bet.HomeOdds * totalInverseOdds);
                var awayPercentage = 1 / (bet.AwayOdds * totalInverseOdds);

                if (homePercentage + awayPercentage < 1)
                {
                    var totalBetAmount = amount; // Cantidad a apostar (puedes ajustar este valor según tus preferencias)
                    var homeBetAmount = totalBetAmount * homePercentage;
                    var awayBetAmount = totalBetAmount * awayPercentage;

                    results.Add($"{bet.Bookmaker}: Apuesta {homeBetAmount.ToString("N2")} a favor de {bet.HomeTeam} " +
                                $"y {awayBetAmount.ToString("N2")} a favor de {bet.AwayTeam}");
                }
            }

            return results;
        }

        public async Task<List<string>> CalculateArbitrageX(List<FootballEvent> bets, decimal amount)
        {
            var results = new List<string>();

            foreach (var bet1 in bets)
            {
                foreach (var bet2 in bets)
                {
                    //if (bet1 != bet2 && bet1.HomeTeam == bet2.AwayTeam && bet1.AwayTeam == bet2.HomeTeam)
                    if (bet1.Bookmaker != bet2.Bookmaker && (bet1.HomeTeam == bet2.HomeTeam && bet1.AwayTeam == bet2.AwayTeam))
                    {
                        var totalInverseOdds1 = 100;
                        var totalInverseOdds2 = 100;

                        var homePercentage1 = 1 / (bet1.HomeOdds * totalInverseOdds1);
                        var awayPercentage1 = 1 / (bet1.AwayOdds * totalInverseOdds1);

                        var homePercentage2 = 1 / (bet2.HomeOdds * totalInverseOdds2);
                        var awayPercentage2 = 1 / (bet2.AwayOdds * totalInverseOdds2);

                        if (homePercentage1 + awayPercentage2 < 1 && homePercentage2 + awayPercentage1 < 1)
                        {
                            // amount - Cantidad a apostar (puedes ajustar este valor según tus preferencias)
                            var homeBetAmount1 = amount * homePercentage1;
                            var awayBetAmount1 = amount * awayPercentage1;
                            var homeBetAmount2 = amount * homePercentage2;
                            var awayBetAmount2 = amount * awayPercentage2;

                            results.Add($"Apuesta segura encontrada:");
                            results.Add($"Casa de apuestas 1 ({bet1.Bookmaker}): Apuesta {homeBetAmount1.ToString("N2")} a favor de {bet1.HomeTeam} " +
                                        $"y {awayBetAmount1.ToString("N2")} a favor de {bet1.AwayTeam}");
                            results.Add($"Casa de apuestas 2 ({bet2.Bookmaker}): Apuesta {homeBetAmount2.ToString("N2")} a favor de {bet2.HomeTeam} " +
                                        $"y {awayBetAmount2.ToString("N2")} a favor de {bet2.AwayTeam}");
                            return results;
                        }
                    }
                }
            }

            results.Add("No se encontraron oportunidades de arbitraje.");
            return results;
        }













        //public class ArbitrageCalculator
        //{
        //    private List<SportBet> bets;
        //    private double capital;

        //    public ArbitrageCalculator(List<SportBet> bets, double capital)
        //    {
        //        this.bets = bets;
        //        this.capital = capital;
        //    }

        //    public void PerformArbitrage()
        //    {
        //        Console.WriteLine("----- PASO 1: Cálculos iniciales -----");

        //        List<double> homeProbabilities = new List<double>();
        //        List<double> awayProbabilities = new List<double>();
        //        List<double> homeInverseOdds = new List<double>();
        //        List<double> awayInverseOdds = new List<double>();
        //        List<double> totalInverseOdds = new List<double>();
        //        List<double> homeProfitPercentages = new List<double>();
        //        List<double> awayProfitPercentages = new List<double>();

        //        foreach (var bet in bets)
        //        {
        //            double homeProbability = 1 / bet.HomeOdd;
        //            double awayProbability = 1 / bet.AwayOdd;
        //            double homeInverseOdd = 1 / bet.HomeOdd;
        //            double awayInverseOdd = 1 / bet.AwayOdd;
        //            double totalInverseOdd = homeInverseOdd + awayInverseOdd;
        //            double homeProfitPercentage = homeInverseOdd / totalInverseOdd;
        //            double awayProfitPercentage = awayInverseOdd / totalInverseOdd;

        //            homeProbabilities.Add(homeProbability);
        //            awayProbabilities.Add(awayProbability);
        //            homeInverseOdds.Add(homeInverseOdd);
        //            awayInverseOdds.Add(awayInverseOdd);
        //            totalInverseOdds.Add(totalInverseOdd);
        //            homeProfitPercentages.Add(homeProfitPercentage);
        //            awayProfitPercentages.Add(awayProfitPercentage);
        //        }

        //        Console.WriteLine();

        //        Console.WriteLine("----- PASO 2: Búsqueda de oportunidades de arbitraje -----");

        //        bool arbitrageFound = false;
        //        int bet1Index = -1;
        //        int bet2Index = -1;

        //        for (int i = 0; i < bets.Count; i++)
        //        {
        //            for (int j = i + 1; j < bets.Count; j++)
        //            {
        //                if (bets[i].HomeTeam == bets[j].AwayTeam && bets[i].AwayTeam == bets[j].HomeTeam)
        //                {
        //                    double homeProfitSum = homeProfitPercentages[i] + awayProfitPercentages[j];
        //                    double awayProfitSum = awayProfitPercentages[i] + homeProfitPercentages[j];

        //                    if (homeProfitSum < 1 && awayProfitSum < 1)
        //                    {
        //                        arbitrageFound = true;
        //                        bet1Index = i;
        //                        bet2Index = j;
        //                        break;
        //                    }
        //                }
        //            }

        //            if (arbitrageFound)
        //                break;
        //        }

        //        Console.WriteLine();

        //        Console.WriteLine("----- PASO 3: Cálculo de cantidades y ganancias -----");

        //        if (arbitrageFound)
        //        {
        //            double bet1Amount = capital * homeProfitPercentages[bet1Index];
        //            double bet2Amount = capital * awayProfitPercentages[bet2Index];
        //            double bet1Profit = bet1Amount * homeInverseOdds[bet1Index];
        //            double bet2Profit = bet2Amount * awayInverseOdds[bet2Index];
        //            double totalProfit = bet1Profit + bet2Profit;

        //            Console.WriteLine($"Apuesta segura encontrada:");

        //            Console.WriteLine($"Casa de apuestas 1 ({bets[bet1Index].Bookmaker}): Apuesta {bet1Amount.ToString("N2")} a favor de {bets[bet1Index].HomeTeam}");
        //            Console.WriteLine($"Casa de apuestas 2 ({bets[bet2Index].Bookmaker}): Apuesta {bet2Amount.ToString("N2")} a favor de {bets[bet2Index].AwayTeam}");

        //            Console.WriteLine();

        //            Console.WriteLine($"Ganancia esperada:");
        //            Console.WriteLine($"- Si gana {bets[bet1Index].HomeTeam}: {bet1Profit.ToString("N2")}");
        //            Console.WriteLine($"- Si gana {bets[bet2Index].AwayTeam}: {bet2Profit.ToString("N2")}");

        //            Console.WriteLine();

        //            Console.WriteLine($"Ganancia total esperada: {totalProfit.ToString("N2")}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("No se encontraron oportunidades de arbitraje.");
        //        }
        //    }
        //}

        //public class Program
        //{
        //    public static void Main(string[] args)
        //    {
        //        List<SportBet> bets = new List<SportBet>()
        //{
        //    new SportBet
        //    {
        //        Bookmaker = "casa1",
        //        HomeTeam = "barcelona",
        //        AwayTeam = "emelec",
        //        HomeOdd = 1.72,
        //        AwayOdd = 5
        //    },
        //    new SportBet
        //    {
        //        Bookmaker = "casa2",
        //        HomeTeam = "barcelona",
        //        AwayTeam = "emelec",
        //        HomeOdd = 1.73,
        //        AwayOdd = 4.86
        //    },
        //    new SportBet
        //    {
        //        Bookmaker = "casa3",
        //        HomeTeam = "barcelona",
        //        AwayTeam = "emelec",
        //        HomeOdd = 1.7,
        //        AwayOdd = 6
        //    }
        //};

        //        double capital = 100;

        //        ArbitrageCalculator calculator = new ArbitrageCalculator(bets, capital);
        //        calculator.PerformArbitrage();
        //    }
        //}




    }
}