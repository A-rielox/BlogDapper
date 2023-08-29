using BlogDapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogDapper.Repositorio;

public interface IEtiquetaRepositorio
{
    Etiqueta GetEtiqueta(int id);
    List<Etiqueta> GetEtiquetas();
    Etiqueta CrearEtiqueta(Etiqueta etiqueta);
    Etiqueta ActualizarEtiqueta(Etiqueta etiqueta);
    void BorrarEtiqueta(int id);

    //Método especial para el dropdown con la lista de etiquetas en artículo
    IEnumerable<SelectListItem> GetListaEtiquetas();

    //Método especial para la acción de asignar etiquetas


    //Método especial para obtener los articulos con las etiquetas asignadas
    List<Articulo> GetArticuloEtiquetas();

    //Método especial para la acción de asignar etiquetas
    //ArticuloEtiquetas AsignarEtiquetas(ArticuloEtiquetas etiqueta);
}
