namespace AutoImperialDAO.Models;

public partial class Modelo
{
    public int idModelo { get; set; }

    public string nombre { get; set; } = null!;

    public int idMarca { get; set; }

    public virtual ICollection<Version> Version { get; set; } = new List<Version>();

    public virtual Marca idMarcaNavigation { get; set; } = null!;
}
