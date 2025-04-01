using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Reserva
{
    public int idReserva { get; set; }

    public DateTime fechaReserva { get; set; }

    public decimal? montoEnganche { get; set; }

    public string? notasAdicionales { get; set; }

    public int idVendedor { get; set; }

    public int idCliente { get; set; }

    public int idVersion { get; set; }

    public string estado { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual Cliente idClienteNavigation { get; set; } = null!;

    public virtual Vendedor idVendedorNavigation { get; set; } = null!;

    public virtual Version idVersionNavigation { get; set; } = null!;
}
