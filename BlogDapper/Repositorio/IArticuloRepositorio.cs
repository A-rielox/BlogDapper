﻿using BlogDapper.Models;

namespace BlogDapper.Repositorio;

public interface IArticuloRepositorio
{
    Articulo GetArticulo(int id);
    List<Articulo> GetArticulos();
    Articulo CrearArticulo(Articulo articulo);
    Articulo ActualizarArticulo(Articulo articulo);
    void BorrarArticulo(int id);

    //Se agregar nuevo método para obtener la relación entre artículo y categoria
    List<Articulo> GetArticuloCategoria();

}
