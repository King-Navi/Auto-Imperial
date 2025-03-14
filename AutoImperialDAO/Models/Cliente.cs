using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Cliente
{
    public int idCliente { get; set; }

    public string nombre { get; set; } = null!;

    public string apellidoPaterno { get; set; } = null!;

    public string apellidoMaterno { get; set; } = null!;

    public string? telefono { get; set; }

    public string? correo { get; set; }

    public string? calle { get; set; }

    public int? numero { get; set; }

    public string? codigoPostal { get; set; }

    public string? ciudad { get; set; }

    public string? RFC { get; set; }

    public string CURP { get; set; } = null!;

    public string estado { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
