namespace AutoImperialDAO.Models;

public partial class Fotos
{
    public int idFotos { get; set; }

    public byte[] foto { get; set; } = null!;

    public int idVehiculo { get; set; }

    public virtual Vehiculo idVehiculoNavigation { get; set; } = null!;
}
