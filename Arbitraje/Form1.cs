using Arbitraje.Models;
using Newtonsoft.Json;

namespace Arbitraje
{
    public partial class Form1 : Form
    {
        string apiKey = "3f415e1e00c15eea72cf0cf501ac225a";
        string baseUrl = "https://api.the-odds-api.com/v4";
        HttpClient _client;

        List<Sport> _listaDeportes;
        List<BettingHouse> _casasDeApuestas;
        List<BettingMarkets> _marcadores;
        List<Game> _juegos;

        public Form1()
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
            cbxBookmarkers.DisplayMember = nameof(BettingHouse.bookmaker);

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
            string sport = cbxSports.SelectedValue.ToString();
            string regions = _casasDeApuestas.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString()).region_key; // se puede concatenar
            string markets = cbxBettingMarket.SelectedValue.ToString(); // se puede concatenar

            _juegos = await GetGames(baseUrl, apiKey, sport, regions, markets);
            if (_juegos.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de juegos");
                return;
            }

            // games by bookmarket
            var selectedBookmaker = _casasDeApuestas.Find(x => x.bookmaker_key == cbxBookmarkers.SelectedValue.ToString());

            // obtiene todos los juegos cuyo bookmaker sea "xxx", la lista incluye todos los bookmaker
            //var gamesFromBookmaker = _juegos.Where(game => game.bookmakers.Any(bookmaker => bookmaker.key == selectedBookmaker.bookmaker_key))
            //.ToList();

            var hoy = DateTime.Now;

            // obtiene todos los juegos cuyo bookmaker sea "xxx", conserva el unico bookmaker "xxx"
            // de hoy 
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
                        .Where(bookmaker => bookmaker.key == selectedBookmaker.bookmaker_key)
                        .ToList()
                })
                .Where(game => game.bookmakers.Any() && game.commence_time <= new DateTime(hoy.Year, hoy.Month, 6, 23, 59, 59))
                .ToList();


            // Mostrar los juegos filtrados (todo)
            foreach (var game in gamesFromBookmaker)
            {
                txtResponse.AppendText($"ID: {game.id}\n");
                txtResponse.AppendText($"Home Team: {game.home_team}\n");
                txtResponse.AppendText($"Away Team: {game.away_team}\n");
                txtResponse.AppendText($"Commence Time: {game.commence_time}\n");
                txtResponse.AppendText("\n");
                foreach (var bookmaker in game.bookmakers)
                { 
                    foreach (var market in bookmaker.markets)
                    { 
                        txtResponse.AppendText($"    Key: {cbxBettingMarket.Text}\n"); // market.key
                        txtResponse.AppendText($"    Last Update: {market.last_update}\n");
                        txtResponse.AppendText("    Outcomes:\n");
                        foreach (var outcome in market.outcomes)
                        {
                            txtResponse.AppendText($"      Name: {outcome.name}\n");
                            txtResponse.AppendText($"      Price: {outcome.price}\n");
                        }
                    }
                }

                txtResponse.AppendText("\n\n\n");
            }

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

                // Leer del TXT
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

        private async Task<List<Game>> GetGames(string baseUrl, string ApiKey, string sport, string regions, string markets)
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

                // Leer del TXT
                string filePath = "soccer_conmebol_copa_libertadores.txt";
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
    }
}