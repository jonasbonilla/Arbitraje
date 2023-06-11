using Arbitraje.Models;
using Newtonsoft.Json;

namespace Arbitraje
{
    public partial class Odds : Form
    {
        #region Variables / Objetos
        string apiKey = "3f415e1e00c15eea72cf0cf501ac225a";
        string baseUrl = "https://api.the-odds-api.com/v4";
        HttpClient _client;

        List<Sport> _listSports;
        List<BettingHouse> _listBettingHouse;
        List<BettingMarkets> _listBettingMarkets;
        List<Game> _listGames;

        List<string> _regiones;
        List<string> _bmNames;
        #endregion

        #region Constructor
        public Odds()
        {
            InitializeComponent();

            var hoy = DateTime.Now;
            _client = new HttpClient();

            _listSports = new List<Sport>();
            _listBettingHouse = new List<BettingHouse>();
            _listBettingMarkets = new List<BettingMarkets>();
            _listGames = new List<Game>();

            _regiones = new List<string>();
            _bmNames = new List<string>();

            dtpDateFrom.Value = new DateTime(hoy.Year, hoy.Month, hoy.Day, 0, 0, 0);

            dtpDateTo.Value = new DateTime(hoy.Year, hoy.Month, hoy.Day + 1, 0, 0, 0);
            pnlLoading.BringToFront();
        }
        #endregion

        #region Formulario / Controles
        private async void Form1_Load(object sender, EventArgs e)
        {
            pnlLoading.Visible = true;
            await Inicializar();
            pnlLoading.Visible = false;
        }

        private async Task Inicializar()
        {
            // obtener casas de apuesta
            _regiones.Clear();
            _listBettingHouse = (await GetBettingHouses()).OrderBy(x => x.bookmaker_key).ThenBy(x => x.region_key).ToList();
            foreach (var bookmark in _listBettingHouse) chkBookmarkers.Items.Add(bookmark.DisplayName);

            // regiones
            cbxRegiones.Items.Add("-- TODOS --");
            var allRegions = _listBettingHouse.OrderBy(x => x.region_key).Select(b => b.region_key).Distinct().ToList();
            foreach (var region in allRegions) cbxRegiones.Items.Add($"{region.ToUpper()} - {RegionToCountry(region)}");
            cbxRegiones.SelectedIndex = 0;

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
        }

        private void cbxGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            // deportes
            if (cbxGroup.SelectedValue == null) return;
            cbxSports.DataSource = _listSports.OrderBy(x => x.description).ToList().FindAll(x => x.group.ToUpper().Equals(cbxGroup.SelectedValue.ToString().ToUpper()));
            cbxSports.ValueMember = nameof(Sport.key);
            cbxSports.DisplayMember = nameof(Sport.description);
        }

        private async void cbxRegiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkBookmarkers.Items.Clear();
            if (cbxRegiones.SelectedItem == null) return;
            var filteredBttingHouses = cbxRegiones.SelectedIndex == 0 ? _listBettingHouse :
                _listBettingHouse.FindAll(x => x.region_key == cbxRegiones.SelectedItem?.ToString().Split('-')[0].Trim().ToLower());
            foreach (var bookmark in filteredBttingHouses) chkBookmarkers.Items.Add(bookmark.DisplayName);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            toolProgressBar.Value = 100;

            ObtenerChkBookmarkers();

            if (cbxGroup.SelectedValue == null || cbxBettingMarket.SelectedValue == null ||
                string.IsNullOrWhiteSpace(lblRegiones.Text.Trim()) || _bmNames.Count == 0)
            {
                MessageBox.Show("Verifíque sus parámetros antes de realizar la búsqueda.");
                return;
            }

            // Ejemplo de uso del método GetOdds con valores personalizados
            string sport = cbxSports.SelectedValue.ToString(); // UN SOLO GRUPO, UNA SOLA CATEGORIA 
            string markets = cbxBettingMarket.SelectedValue.ToString(); // H2H - se puede concatenar

            // consultamos juegos
            txtResponse.Clear();
            _listGames = await GetGames(baseUrl, apiKey, sport, lblRegiones.Text.Trim(), markets);
            if (_listGames.Count == 0)
            {
                MessageBox.Show("No se ha encontrado juegos disponibles para su búsqueda");
                return;
            }

            // filtro los eventos segun mis especificaciones
            var gamesFromBookmaker = _listGames.Select(game =>
            {
                game.id = game.id;
                game.sport_key = game.sport_key;
                game.sport_title = game.sport_title;
                game.commence_time = game.commence_time;
                game.home_team = game.home_team;
                game.away_team = game.away_team;
                game.bookmakers = game.bookmakers.Where(bookmarker => _bmNames.Contains(bookmarker.key)).ToList();
                return game;
            })
            .Where(game => game.bookmakers.Count > 0 && (game.commence_time >= dtpDateFrom.Value && game.commence_time <= dtpDateTo.Value))
            .ToList();

            // validaciones
            if (gamesFromBookmaker.Count == 0)
            {
                txtResponse.AppendText("No se encontraron eventos para las opciones seleccionadas");
                return;
            }

            //  Armar la lista los juegos filtrados
            var eventos = new List<SportEvent>();
            foreach (Game game in gamesFromBookmaker)
            {
                foreach (Bookmaker bookmaker in game.bookmakers)
                {
                    eventos.Add(new SportEvent
                    {
                        Bookmaker = bookmaker.key,
                        CommenceTime = game.commence_time,
                        HomeTeam = game.home_team.Trim(),
                        AwayTeam = game.away_team.Trim(),
                        HomeOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == game.home_team.Trim()).price,
                        AwayOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == game.away_team.Trim()).price,
                        DrawOdds = bookmaker.markets.Find(x => x.key == cbxBettingMarket.SelectedValue.ToString()).outcomes.Find(x => x.name == "Draw").price,
                    });
                }
            }

            // presento eventos 
            foreach (SportEvent evento in eventos)
            {
                txtResponse.AppendText($"{evento.Bookmaker.ToUpper()} - {evento.CommenceTime.ToLongDateString()} / {evento.CommenceTime.ToLongTimeString()} \n");
                txtResponse.AppendText($"{evento.HomeTeam} vs. {evento.AwayTeam}\n");
                txtResponse.AppendText($"HomeOdds: {evento.HomeOdds}\n");
                txtResponse.AppendText($"AwayOdds: {evento.AwayOdds}\n");
                txtResponse.AppendText("\n\n");
            }

            toolProgressBar.Value = 0;
        }
        #endregion

        #region Funciones / Metodos
        private string RegionToCountry(string region)
        {
            var country = string.Empty;
            return country = region.ToLower() switch
            {
                "us" => "Estados Unidos",
                "eu" => "Unión Europea",
                "au" => "Australia",
                "uk" => "Reino Unido",
                "cn" => "China",
                "ru" => "Rusia",
                "de" => "Alemania",
                "fr" => "Francia",
                "jp" => "Japón",
                "br" => "Brasil",
                "ca" => "Canadá",
                "in" => "India",
                "it" => "Italia",
                "es" => "España",
                "mx" => "México",
                "kr" => "Corea del Sur",
                "sa" => "Arabia Saudita",
                "za" => "Sudáfrica",
                "ar" => "Argentina",
                "co" => "Colombia",
                "ec" => "Ecuador",
                "eg" => "Egipto",
                _ => "Desconocido"
            };
        }

        private void ObtenerChkBookmarkers()
        {
            _regiones.Clear();
            _bmNames.Clear();
            lblRegiones.Text = string.Empty;
            string bmKey;
            string bmName;
            string region;

            // para agregar bookmarkers
            foreach (var check in chkBookmarkers.CheckedItems)
            {
                bmName = check.ToString().Split('(')[0].Trim().ToLower();
                region = check.ToString().Split('(')[1].Replace(')', ' ').Trim().ToLower();
                bmKey = _listBettingHouse.Find(x => x.bookmaker.Trim().ToLower() == bmName && x.region_key.Trim().ToLower() == region).bookmaker_key.Trim().ToLower();

                if (!_regiones.Contains(region))
                {
                    _regiones.Add(region);
                    lblRegiones.Text += $"{region},";
                }
                if (!_bmNames.Contains(bmKey)) _bmNames.Add(bmKey);
            }
            if (lblRegiones.Text.EndsWith(',')) lblRegiones.Text = lblRegiones.Text.Substring(0, lblRegiones.Text.Length - 1);
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
                    // Ejemplo:
                    // https://api.the-odds-api.com/v4/sports/soccer_conmebol_copa_libertadores/odds/?apiKey=3f415e1e00c15eea72cf0cf501ac225a&regions=eu&markets=h2h
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
        #endregion
    }
}