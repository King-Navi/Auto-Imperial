using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportClient
{
    public class ClientReport
    {
        [JsonPropertyName("vendedor")]
        public string Salesperson { get; set; }

        [JsonPropertyName("fecha_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("fecha_fin")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("nombre_completo")]
        public string FullName { get; set; }

        [JsonPropertyName("rfc")]
        public string RFC { get; set; }

        [JsonPropertyName("curp")]
        public string CURP { get; set; }

        [JsonPropertyName("compras")]
        public List<ClientPurchase> Purchases { get; set; }
    }
}
