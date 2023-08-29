using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models;

public class ArticuloEtiquetas
{
    [Key]
    public int IdArticulo { get; set; }

    [Key]
    public int IdEtiqueta { get; set; }
}
