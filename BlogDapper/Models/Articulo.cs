using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models;

public class Articulo
{
    //public Articulo() {
    //    Etiqueta = new List<Etiqueta>();
    //}

    [Key]
    public int IdArticulo { get; set; }

    [Required(ErrorMessage = "El Título es obligatorio")]
    public string Titulo { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "La descripción debe tener mínimo 10 caracteres y máximo 1000")]
    public string Descripcion { get; set; }

    public string Imagen { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Llave foránea ( si la esta mapeando )
    [Required(ErrorMessage = "El nombre de categoría es obligatorio")]
    public int CategoriaId { get; set; }



    //Esta indica la relación con categoría de que un artículo debe pertenecer a uno sola categoría
    public virtual Categoria Categoria { get; set; }

    public List<Comentario> Comentario { get; set; }

    //public List<Etiqueta> Etiqueta { get; set; }
}

// la relacion uno a muchos
// aca
// public virtual Categoria Categoria { get; set; }

// en categoria
// public List<Articulo> Articulo { get; set; }