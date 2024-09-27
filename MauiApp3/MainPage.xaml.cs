using System.Collections.ObjectModel;
using System.Text.Json;

namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public List<Cidade> Cidades { get; set;  } = new List<Cidade>();   

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnGetClicked(object sender, EventArgs e)
        {
            List<Cidade> cidades = new List<Cidade>();
            HttpClient client = new HttpClient();
            await GetCidades(client);

            Console.WriteLine(Cidades.Count);
        }
        private async void CarregarCidadesAsync()
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions _serializeOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }

        async Task GetCidades( HttpClient client)
        {
            JsonSerializerOptions _serializeOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string url = "https://localhost:7096/Cidade/select";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream =  response.Content.ReadAsStream())
                {
                    var data = JsonSerializer.Deserialize<List<Cidade>>(responseStream, _serializeOptions);
                    Cidades = data;
                    
                }
            }
        }
        static async Task GetFile(HttpClient client)
        {
            string cidadeString = File.ReadAllText("c:\\temp\\cidades.json");
            List<Cid>? cidades =
                   JsonSerializer.Deserialize<List<Cid>>(cidadeString);
            Console.WriteLine(cidades[0].nmcidade);
            Console.ReadKey();
        }
        static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5058/api/Cidade/buscartodos/1");
            response.EnsureSuccessStatusCode().WriteRequestToConsole();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var data = JsonSerializer.Deserialize<List<Cidade>>(responseStream);

            Console.WriteLine($"{jsonResponse}\n");
            Console.Read();
        }
        
    }

    static class HttpResponseMessageExtensions
    {
        internal static void WriteRequestToConsole(this HttpResponseMessage response)
        {
            if (response is null)
            {
                return;
            }

            var request = response.RequestMessage;
            Console.Write($"{request?.Method} ");
            Console.Write($"{request?.RequestUri} ");
            Console.WriteLine($"HTTP/{request?.Version}");
        }
    }

}
