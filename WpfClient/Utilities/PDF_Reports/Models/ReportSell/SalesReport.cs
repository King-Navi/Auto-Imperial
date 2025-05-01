using System.Text.Json.Serialization;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportSell
{
    public class SalesReport
    {
        [JsonPropertyName("vendedor")]
        public string Salesperson { get; set; }
        [JsonPropertyName("fecha_inicio")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("fecha_fin")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("ventas")]
        public List<Sale> Sales { get; set; }
    }
}
