using Arbitraje.Models;
using Newtonsoft.Json; 

namespace Arbitraje
{
    public partial class Odds : Form
    {
        string apiKey = "3f415e1e00c15eea72cf0cf501ac225a";
        string baseUrl = "https://api.the-odds-api.com/v4";
        HttpClient _client;

        List<Sport> _listaDeportes;
        List<BettingHouse> _casasDeApuestas;
        List<BettingMarkets> _marcadores;
        List<Game> _juegos;

        public Odds()
        {
            InitializeComponent();
            pnlLoading.BringToFront();

            _client = new HttpClient();
        }

        private async Task Inicializar()
        {
            // obtener casas de apuesta
            _casasDeApuestas = await GetBettingHouses();
            cbxBookmarkers.DataSource = _casasDeApuestas.OrderBy(x => x.bookmaker).ToList();
            cbxBookmarkers.ValueMember = nameof(BettingHouse.bookmaker_key);
            cbxBookmarkers.DisplayMember = nameof(BettingHouse.DisplayName);

            // obtener marcadores
            _marcadores = await GetBettingMarkets();
            cbxBettingMarket.DataSource = _marcadores.OrderBy(x => x.market_name).ToList();
            cbxBettingMarket.ValueMember = nameof(BettingMarkets.market_key);
            cbxBettingMarket.DisplayMember = nameof(BettingMarkets.market_name);

            // Obtener deportes
            _listaDeportes = await GetSports(baseUrl, apiKey);
            if (_listaDeportes.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de deportes");
                return;
            }

            var grupos = _listaDeportes.GroupBy(x => x.group).ToList();

            // grupos
            cbxGroup.DataSource = grupos;
            cbxGroup.ValueMember = nameof(Sport.key);
            cbxGroup.DisplayMember = nameof(Sport.description);
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
            cbxSports.DataSource = _listaDeportes.FindAll(x => x.group.ToUpper().Equals(cbxGroup.SelectedValue.ToString().ToUpper()));
            cbxSports.ValueMember = nameof(Sport.key);
            cbxSports.DisplayMember = nameof(Sport.description);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (cbxGroup.SelectedValue == null || cbxBookmarkers.SelectedValue == null || cbxBettingMarket.SelectedValue == null)
            {
                MessageBox.Show("Error al cargar datos");
                return;
            }

            // Ejemplo de uso del método GetOdds con valores personalizados
            string sport = cbxSports.SelectedValue.ToString(); // UN SOLO GRUPO, UNA SOLA CATEGORIA
            string regions = _casasDeApuestas.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString()).region_key; // eu - se puede concatenar
            string markets = cbxBettingMarket.SelectedValue.ToString(); // H2H - se puede concatenar

            // consultamos juegos
            txtResponse.Clear();
            _juegos = await GetGames(baseUrl, apiKey, sport, regions, markets);
            if (_juegos.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de juegos");
                return;
            }

            // filtramos games by bookmarket - obtiene todos los juegos cuyo bookmaker sea "xxx"
            var hoy = DateTime.Now;
            var selectedBookmaker = _casasDeApuestas.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString());
            var gamesFromBookmaker = _juegos
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

            pnlLoading.Visible = true;
            var resultp = await ShowProbabilities(eventos);
            txtResponse.AppendText(resultp);
            
            txtResponse.AppendText("\n\n");
            txtResponse.AppendText("\n\n");


            // arbitraje para la misma casa
            var arbitrageResults1 = await CalculateArbitrage1(eventos, 100m);
            foreach (var result in arbitrageResults1) txtResponse.AppendText(result);

            txtResponse.AppendText("\n\n");
            txtResponse.AppendText("\n\n");

            // arbitraje para la diferentes casas
            var arbitrageResults2 = await CalculateArbitrageX(eventos, 100m);
            foreach (var result in arbitrageResults2) txtResponse.AppendText(result);

            pnlLoading.Visible = false;
        }

        private async Task<List<BettingHouse>> GetBettingHouses()
        {
            // https://the-odds-api.com/sports-odds-data/bookmaker-apis.html
            List<BettingHouse> result;
            try
            {
                string filePath = "betting_houses.txt";
                string json = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<List<BettingHouse>>(json);
            }
            catch (Exception ex)
            {
                result = new List<BettingHouse>();
                Console.WriteLine("Error on GetBookMarkers: " + ex.Message);
            }

            // Devolvemos una lista vacía por simplicidad
            return result;
        }

        private async Task<List<BettingMarkets>> GetBettingMarkets()
        {
            // https://the-odds-api.com/sports-odds-data/betting-markets.html
            List<BettingMarkets> result;
            try
            {
                string filePath = "betting_markets.txt";
                string json = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<List<BettingMarkets>>(json);
            }
            catch (Exception ex)
            {
                result = new List<BettingMarkets>();
                Console.WriteLine("Error on GetBettingMarkets: " + ex.Message);
            }

            // Devolvemos una lista vacía por simplicidad
            return result;
        }

        private async Task<List<Sport>> GetSports(string baseUrl, string ApiKey)
        {
            List<Sport> result;
            try
            {
                //// Por API
                //var url = $"{baseUrl}/sports/?apiKey={ApiKey}";
                //var response = await _client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string json = await response.Content.ReadAsStringAsync();

                //// Escribir al TXT
                //string filePath = "sports.txt"; // Specify the file path
                //File.WriteAllText(filePath, json);

                //// Leer del TXT
                string filePath = "sports.txt";
                string json = File.ReadAllText(filePath);

                result = JsonConvert.DeserializeObject<List<Sport>>(json);
            }
            catch (Exception ex)
            {
                result = new List<Sport>();
                Console.WriteLine("Error on GetSports: " + ex.Message);
            }

            // Devolvemos una lista vacía por simplicidad
            return result;
        }

        private async Task<List<Game>> GetGames(string baseUrl, string apiKey, string sport, string regions, string markets)
        {
            List<Game> result;
            try
            {
                //// Por API
                //var url = $"{baseUrl}/sports/{sport}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
                //var response = await _client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string json = await response.Content.ReadAsStringAsync();

                //// Escribir al TXT
                //string filePath = "soccer_conmebol_copa_libertadores.txt"; // Specify the file path
                //File.WriteAllText(filePath, json);

                //// Leer del TXT
                string filePath = $"1_{sport}.txt";
                string json = File.ReadAllText(filePath);

                result = JsonConvert.DeserializeObject<List<Game>>(json);
            }
            catch (Exception ex)
            {
                result = new List<Game>();
                Console.WriteLine("Error on GetGames: " + ex.Message);
            }

            // Devolvemos una lista vacía por simplicidad
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
    }
}