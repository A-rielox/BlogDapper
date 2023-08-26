using BlogDapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogDapper.Repositorio;

public interface ICategoriaRepositorio
{
    Categoria GetCategoria(int id);
    List<Categoria> GetCategorias();
    Categoria CrearCategoria(Categoria categoria);
    Categoria ActualizarCategoria(Categoria categoria);
    void BorrarCategoria(int id);

    //Método especial para el dropdown con la lista de categorías en la vista artículos,
    //se debe crear aquí para invocarse desde el controlador artículos
    IEnumerable<SelectListItem> GetListaCategorias();
}
