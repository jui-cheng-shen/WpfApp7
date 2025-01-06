namespace _2024_WpfApp7
{
    // 額外資訊類別
    public class Extras
    {
        // API 金鑰
        public string api_key { get; set; }
    }

    // 欄位資訊類別
    public class Field
    {
        // 欄位 ID
        public string id { get; set; }
        // 欄位類型
        public string type { get; set; }
        // 欄位詳細資訊
        public Info info { get; set; }
    }

    // 欄位詳細資訊類別
    public class Info
    {
        // 標籤
        public string label { get; set; }
    }

    // 連結資訊類別
    public class Links
    {
        // 起始連結
        public string start { get; set; }
        // 下一頁連結
        public string next { get; set; }
    }

    // 記錄類別
    public class Record
    {
        // 站點名稱
        public string sitename { get; set; }
        // 縣市
        public string county { get; set; }
        // 空氣品質指標
        public string aqi { get; set; }
        // 污染物
        public string pollutant { get; set; }
        // 狀態
        public string status { get; set; }
        // 二氧化硫
        public string so2 { get; set; }
        // 一氧化碳
        public string co { get; set; }
        // 臭氧
        public string o3 { get; set; }
        // 8小時臭氧
        public string o3_8hr { get; set; }
        // 懸浮微粒 PM10
        public string pm10 { get; set; }
        // 懸浮微粒 PM2.5
        public string pm25 { get; set; }
        // 二氧化氮
        public string no2 { get; set; }
        // 氮氧化物
        public string nox { get; set; }
        // 一氧化氮
        public string no { get; set; }
        // 風速
        public string wind_speed { get; set; }
        // 風向
        public string wind_direc { get; set; }
        // 發佈時間
        public string publishtime { get; set; }
        // 8小時一氧化碳
        public string co_8hr { get; set; }
        // PM2.5 平均值
        public string pm25_avg { get; set; }
        // PM10 平均值
        public string pm10_avg { get; set; }
        // 二氧化硫平均值
        public string so2_avg { get; set; }
        // 經度
        public string longitude { get; set; }
        // 緯度
        public string latitude { get; set; }
        // 站點 ID
        public string siteid { get; set; }
    }

    // AQI 資料類別
    public class AQIdata
    {
        // 欄位列表
        public List<Field> fields { get; set; }
        // 資源 ID
        public string resource_id { get; set; }
        // 額外資訊
        public Extras __extras { get; set; }
        // 是否包含總數
        public bool include_total { get; set; }
        // 總數
        public string total { get; set; }
        // 資源格式
        public string resource_format { get; set; }
        // 限制
        public string limit { get; set; }
        // 偏移量
        public string offset { get; set; }
        // 連結資訊
        public Links _links { get; set; }
        // 記錄列表
        public List<Record> records { get; set; }
    }
}
