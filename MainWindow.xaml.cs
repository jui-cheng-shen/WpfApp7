using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Media3D;

namespace _2024_WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string aqiURL = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=e8dd42e6-9b8b-43f8-991e-b3dee723a52d&limit=1000&sort=ImportDate%20desc&format=JSON";

        AQIdata aqiData = new AQIdata();
        List<Field> fields = new List<Field>();
        List<Record> records = new List<Record>();
        public MainWindow()
        {
            InitializeComponent();
            urlTextBox.Text = aqiURL;
        }

        private async void btnGetAQI_Click(object sender, RoutedEventArgs e)
        {
            string url = urlTextBox.Text;
            ContentTextBox.Text = "抓取資料中...";

            string data = await GetAQIAsync(url);
            ContentTextBox.Text = data;

            aqiData = JsonSerializer.Deserialize<AQIdata>(data);
        }

        private async Task<string> GetAQIAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}