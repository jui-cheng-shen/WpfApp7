using LiveCharts;
using LiveCharts.Wpf;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace _2024_WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // AQI 資料的 URL
        string aqiURL = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=e8dd42e6-9b8b-43f8-991e-b3dee723a52d&limit=1000&sort=ImportDate%20desc&format=JSON";

        // AQI 資料物件
        AQIdata aqiData = new AQIdata();
        // 欄位列表
        List<Field> fields = new List<Field>();
        // 記錄列表
        List<Record> records = new List<Record>();
        // 圖表系列集合
        SeriesCollection seriesCollection = new SeriesCollection();
        // 選中的記錄列表
        List<Record> selectedRecords = new List<Record>();

        public MainWindow()
        {
            InitializeComponent();
            // 初始化 URL 輸入框
            urlTextBox.Text = aqiURL;
        }

        // 取得 AQI 資料按鈕點擊事件
        private async void btnGetAQI_Click(object sender, RoutedEventArgs e)
        {
            string url = urlTextBox.Text;
            ContentTextBox.Text = "抓取資料中...";

            // 非同步取得 AQI 資料
            string data = await GetAQIAsync(url);
            ContentTextBox.Text = data;

            // 反序列化 AQI 資料
            aqiData = JsonSerializer.Deserialize<AQIdata>(data);
            fields = aqiData.fields.ToList();
            records = aqiData.records.ToList();
            selectedRecords = records;
            statusBarText.Text = $"共有{records.Count}筆資料";

            // 顯示 AQI 資料
            DisplayAQIData();
        }

        // 顯示 AQI 資料
        private void DisplayAQIData()
        {
            // 將記錄綁定到 DataGrid
            RecordDataGrid.ItemsSource = records;

            // 取得第一筆記錄
            Record record = records[0];
            DataWrapPanel.Children.Clear();

            // 為每個欄位建立 CheckBox
            foreach (Field field in fields)
            {
                var propertyInfo = record.GetType().GetProperty(field.id);
                if (propertyInfo != null)
                {
                    var value = propertyInfo.GetValue(record) as string;
                    if (double.TryParse(value, out double v))
                    {
                        CheckBox cb = new CheckBox
                        {
                            Content = field.info.label,
                            Tag = field.id,
                            Margin = new Thickness(3),
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                            Width = 120
                        };
                        cb.Checked += UpdateChart;
                        cb.Unchecked += UpdateChart;
                        DataWrapPanel.Children.Add(cb);
                    }
                }
            }
        }

        // 更新圖表
        private void UpdateChart(object sender, RoutedEventArgs e)
        {
            seriesCollection.Clear();

            // 為每個選中的 CheckBox 建立圖表系列
            foreach (CheckBox cb in DataWrapPanel.Children)
            {
                if (cb.IsChecked == true)
                {
                    List<string> labels = new List<string>();
                    string tag = cb.Tag as string;
                    ColumnSeries columnSeries = new ColumnSeries();
                    ChartValues<double> values = new ChartValues<double>();

                    // 取得選中記錄的值
                    foreach (Record r in selectedRecords)
                    {
                        var propertyInfo = r.GetType().GetProperty(tag);
                        if (propertyInfo != null)
                        {
                            var value = propertyInfo.GetValue(r) as string;
                            if (double.TryParse(value, out double v))
                            {
                                labels.Add(r.sitename);
                                values.Add(v);
                            }
                        }
                    }
                    columnSeries.Values = values;
                    columnSeries.Title = tag;
                    columnSeries.LabelPoint = point => $"{labels[(int)point.X]}:{point.Y.ToString()}";
                    seriesCollection.Add(columnSeries);
                }
            }
            AQIChart.Series = seriesCollection;
        }

        // 非同步取得 AQI 資料
        private async Task<string> GetAQIAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    statusBarText.Text = $"Error: {response.StatusCode}";
                    return null;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
        }

        // DataGrid 加載行事件
        private void RecordDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        // DataGrid 選中行改變事件
        private void RecordDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecords = RecordDataGrid.SelectedItems.Cast<Record>().ToList();
            statusBarText.Text = $"共有{selectedRecords.Count}筆資料";
        }
    }
}
