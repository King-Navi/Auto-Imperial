using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Proveedor
{
    public int idProveedor { get; set; }

    public string nombreProveedor { get; set; } = null!;

    public string? calle { get; set; }

    public int? numero { get; set; }

    public string? codigoPostal { get; set; }

    public string? correo { get; set; }

    public string? telefono { get; set; }

    public string? contactoPrincipal { get; set; }

    public string? estado { get; set; }

    public virtual ICollection<CompraProveedor> CompraProveedores { get; set; } = new List<CompraProveedor>();
}
