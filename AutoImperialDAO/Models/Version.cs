using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Version
{
    public int idVersion { get; set; }

    public string nombre { get; set; } = null!;

    public string? transmision { get; set; }

    public int? puertas { get; set; }

    public string? motor { get; set; }

    public int idModelo { get; set; }

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();

    public virtual ICollection<Vehiculo> Vehiculo { get; set; } = new List<Vehiculo>();

    public virtual Modelo idModeloNavigation { get; set; } = null!;
}
