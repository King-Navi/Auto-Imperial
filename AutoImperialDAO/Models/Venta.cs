using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Venta
{
    public int idVenta { get; set; }

    public DateOnly fechaVenta { get; set; }

    public decimal? precioVehiculo { get; set; }

    public string? formaPago { get; set; }

    public string? notasAdicionales { get; set; }

    public int idReserva { get; set; }

    public int idVehiculo { get; set; }

    public string estadoVenta { get; set; } = null!;

    public virtual Reserva idReservaNavigation { get; set; } = null!;

    public virtual Vehiculo idVehiculoNavigation { get; set; } = null!;
}
