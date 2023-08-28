using BlogDapper.Models;

namespace BlogDapper.Repositorio;

public interface IComentarioRepositorio
{
    Comentario GetComentario(int id);
    List<Comentario> GetComentarios();
    Comentario CrearComentario(Comentario Comentario);
    Comentario ActualizarComentario(Comentario Comentario);
    void BorrarComentario(int id);

    //Se agregar despúes como método cuando se vaya a hacer la relación
    List<Comentario> GetComentarioArticulo();
}
