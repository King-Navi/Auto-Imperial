using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class CompraProveedor
{
    public int idCompraProveedor { get; set; }

    public decimal? montoTotal { get; set; }

    public string folio { get; set; } = null!;

    public DateOnly fechaCompra { get; set; }

    public int idAdministrador { get; set; }

    public int idProveedor { get; set; }

    public int vehiculosComprados { get; set; }

    public virtual ICollection<Vehiculo> Vehiculo { get; set; } = new List<Vehiculo>();

    public virtual Administrador idAdministradorNavigation { get; set; } = null!;

    public virtual Proveedor idProveedorNavigation { get; set; } = null!;
}
