using System.Text.Json.Serialization;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportInventory
{
    public class InventoryItem
    {
        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("marcar")]
        public string Brand { get; set; }
    }
}
