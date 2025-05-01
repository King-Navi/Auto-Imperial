namespace AutoImperialDAO.Models;

public partial class Administrador
{
    public int idAdministrador { get; set; }

    public string nombre { get; set; } = null!;

    public string apellidoPaterno { get; set; } = null!;

    public string apellidoMaterno { get; set; } = null!;

    public string? telefono { get; set; }

    public string? correo { get; set; }

    public string? calle { get; set; }

    public int? numero { get; set; }

    public string? codigoPostal { get; set; }

    public string? ciudad { get; set; }

    public string estadoCuenta { get; set; } = null!;

    public string CURP { get; set; } = null!;

    public string RFC { get; set; } = null!;

    public string puestoAdministrador { get; set; } = null!;

    public string nombreUsuario { get; set; } = null!;

    public string password { get; set; } = null!;

    public string numeroEmpleado { get; set; } = null!;

    public string? sucursal { get; set; }

    public virtual ICollection<CompraProveedor> CompraProveedor { get; set; } = new List<CompraProveedor>();
}
