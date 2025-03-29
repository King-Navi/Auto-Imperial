using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
    internal class ReserveCardModel
    {

        private string _nombreCompleto = "N/D";
        public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
    }
}
