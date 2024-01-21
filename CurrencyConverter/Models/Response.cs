namespace CurrencyConverter.Models
{
    public class Response
    {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public string time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; }
        public string time_next_update_unix { get; set; }
        public string time_next_update_utc { get; set; }
        public string base_code { get; set; }
        public string target_code { get; set; }
        public string conversion_rate { get; set; }
        public string conversion_result { get; set; }
        public string base_name { get; set; }
        public string target_name { get; set; }
        public string base_symbol { get; set; }
        public string target_symbol { get; set; }
        public decimal? amount { get; set; }
    }
}