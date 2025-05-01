using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfClient.Utilities.PDF_Reports.Models.ReportFinancial
{
    public class FinancialReport
    {
        [JsonPropertyName("vendedor")]
        public string Salesperson { get; set; }

        [JsonPropertyName("fecha_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("fecha_fin")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("egresos")]
        public decimal Expenses { get; set; }

        [JsonPropertyName("ingresos")]
        public decimal Income { get; set; }

        [JsonPropertyName("total")]
        public decimal Total => Income - Expenses;

        [JsonPropertyName("ventas")]
        public List<FinancialSale> Sales { get; set; }

        [JsonPropertyName("compras")]
        public List<FinancialPurchase> Purchases { get; set; }
    }
}
