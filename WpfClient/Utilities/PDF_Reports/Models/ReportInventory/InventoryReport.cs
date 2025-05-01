using System.Text.Json.Serialization;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportInventory
{
    public class InventoryReport
    {
        [JsonPropertyName("vendedor")]
        public string Salesperson { get; set; }

        [JsonPropertyName("fecha_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("fecha_fin")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("inventario")]
        public List<InventoryItem> Inventory { get; set; }
    }
}
