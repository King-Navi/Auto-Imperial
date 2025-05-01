using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.ModelsDTO
{
    public class FinancialPurchaseDTO
    {
        public string Model { get; set; }
        public string Version { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public decimal Amount { get; set; }
    }
}
