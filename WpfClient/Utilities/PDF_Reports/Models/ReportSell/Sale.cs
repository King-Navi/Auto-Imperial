using System.Text.Json.Serialization;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportSell
{
    public class Sale
    {
        [JsonPropertyName("vendedor")]
        public string Salesperson { get; set; }
        [JsonPropertyName("cliente")]
        public string Customer { get; set; }
        [JsonPropertyName("vehiculo")]
        public string Vehicle { get; set; }
        [JsonPropertyName("tipo")]
        public string Type { get; set; }
        [JsonPropertyName("monto")]
        public decimal Amount { get; set; }
    }
}
