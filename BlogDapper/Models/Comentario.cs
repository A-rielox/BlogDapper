using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models;

public class Comentario
{
    [Key]
    public int IdComentario { get; set; }

    [Required(ErrorMessage = "El Título es obligatorio")]
    public string Titulo { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10, ErrorMessage = "El mensaje debe tener mínimo 10 caracteres y máximo 300")]
    public string Mensaje { get; set; }

    public DateTime FechaCreacion { get; set; }

    //Llave foránea
    public int ArticuloId { get; set; }

    //Esta indica la relación con Articulo de que un Comentario debe pertenecer a un solo Articulo
    public virtual Articulo Articulo { get; set; }

}
