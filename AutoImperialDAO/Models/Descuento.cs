using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Descuento
{
    public int idDescuento { get; set; }

    public DateOnly fechaInicio { get; set; }

    public DateOnly fechaFin { get; set; }

    public int descuentoPorcentaje { get; set; }

    public virtual ICollection<Vehiculo> idVehiculo { get; set; } = new List<Vehiculo>();
}
