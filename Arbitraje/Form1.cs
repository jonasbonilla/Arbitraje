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
        List<Bookmakers> _casasDeApuestas;
        List<BettingMarkets> _marcadores;

        public Form1()
        {
            InitializeComponent();
            pnlLoading.BringToFront();

            _client = new HttpClient();
        }

        private async Task Inicializar()
        {
            // obtener casas de apuesta
            _casasDeApuestas = await GetBookMarkers();
            cbxBookmarkers.DataSource = _casasDeApuestas.OrderBy(x => x.Bookmaker).ToList();
            cbxBookmarkers.ValueMember = nameof(Bookmakers.RegionKey);
            cbxBookmarkers.DisplayMember = nameof(Bookmakers.Bookmaker);

            // obtener marcadores
            _marcadores = await GetBettingMarkets();
            cbxBettingMarket.DataSource = _marcadores.OrderBy(x => x.MarketNames).ToList();
            cbxBettingMarket.ValueMember = nameof(BettingMarkets.MarketKey);
            cbxBettingMarket.DisplayMember = nameof(BettingMarkets.MarketNames);

            // Obtener deportes
            _listaDeportes = await GetSports(baseUrl, apiKey);
            if (_listaDeportes.Count == 0)
            {
                MessageBox.Show("Error al obtenrer lista de deportes");
                return;
            }

            var grupos = _listaDeportes.GroupBy(x => x.Group).ToList();

            // grupos
            cbxGroup.DataSource = grupos;
            cbxGroup.ValueMember = nameof(Sport.Key);
            cbxGroup.DisplayMember = nameof(Sport.Description);
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
            cbxSports.DataSource = _listaDeportes.FindAll(x => x.Group == cbxGroup.SelectedValue);
            cbxSports.ValueMember = nameof(Sport.Key);
            cbxSports.DisplayMember = nameof(Sport.Description);
        }

        private async Task<List<Bookmakers>> GetBookMarkers()
        {
            // https://the-odds-api.com/sports-odds-data/bookmaker-apis.html
            List<Bookmakers> result;
            try
            {
                string filePath = "bookmarkers.txt";
                string json = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<List<Bookmakers>>(json);
            }
            catch (Exception ex)
            {
                result = new List<Bookmakers>();
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
                string filePath = "bettingMarkets.txt";
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
                //var response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<List<Sport>>(json);

                // Por TXT
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

        private async void button1_Click(object sender, EventArgs e)
        {
            // Ejemplo de uso del método GetOdds con valores personalizados
            //string sport = "soccer";
            //string regions = "eu";
            //string markets = "h2h"; 


        }
    }
}