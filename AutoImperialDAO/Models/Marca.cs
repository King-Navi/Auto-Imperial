namespace AutoImperialDAO.Models;

public partial class Marca
{
    public int idMarca { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Modelo> Modelo { get; set; } = new List<Modelo>();
}
