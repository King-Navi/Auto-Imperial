namespace AutoImperialDAO.Models;

public partial class Vehiculo
{
    public int idVehiculo { get; set; }

    public string? tipoVehiculo { get; set; }

    public string? estadoVehiculo { get; set; }

    public decimal? precioProveedor { get; set; }

    public decimal? precioVehiculo { get; set; }

    public int? anio { get; set; }

    public string? color { get; set; }

    public string VIN { get; set; } = null!;

    public string numeroChasis { get; set; } = null!;

    public string numeroMotor { get; set; } = null!;

    public int idCompraProveedor { get; set; }

    public int idVersion { get; set; }

    public virtual ICollection<Fotos> Fotos { get; set; } = new List<Fotos>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual CompraProveedor idCompraProveedorNavigation { get; set; } = null!;

    public virtual Version idVersionNavigation { get; set; } = null!;

    public virtual ICollection<Descuento> idDescuento { get; set; } = new List<Descuento>();
}
