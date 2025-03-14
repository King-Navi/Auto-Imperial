using System;
using System.Collections.Generic;

namespace AutoImperialDAO.Models;

public partial class Marca
{
    public int idMarca { get; set; }

    public string nombre { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
