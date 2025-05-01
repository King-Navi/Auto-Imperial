using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.ModelsDTO
{
    public class SaleData
    {
        public string Salesperson { get; set; }
        public string Customer { get; set; }
        public string Vehicle { get; set; }
        public string Type { get; set; }
        public decimal? Amount { get; set; }
    }
}
