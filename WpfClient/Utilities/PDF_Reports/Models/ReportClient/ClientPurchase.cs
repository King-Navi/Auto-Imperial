using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportClient
{
    public class ClientPurchase
    {
        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("marcar")]
        public string Brand { get; set; }
    }
}
