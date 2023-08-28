using BlogDapper.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace BlogDapper.Repositorio;

public class ComentarioRepositorio : IComentarioRepositorio
{
    private readonly IDbConnection _bd;

    public ComentarioRepositorio(IConfiguration configuration)
    {
        _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Comentario ActualizarComentario(Comentario comentario)
    {
        var sql = "UPDATE Comentario SET Titulo = @Titulo, Mensaje = @Mensaje " +
                  "WHERE IdComentario = @IdComentario";

        _bd.Execute(sql, comentario);

        return comentario;
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public void BorrarComentario(int id)
    {
        var sql = "DELETE FROM Comentario WHERE IdComentario = @IdComentario";

        _bd.Execute(sql, new { IdComentario = id });
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Comentario CrearComentario(Comentario comentario)
    {
        var sql = "INSERT INTO Comentario(Titulo, Mensaje, ArticuloId, FechaCreacion) " +
                  "VALUES (@Titulo, @Mensaje, @ArticuloId, @FechaCreacion)";

        _bd.Execute(sql, new
        {
            comentario.Titulo,
            comentario.Mensaje,
            comentario.ArticuloId,
            FechaCreacion = DateTime.Now
        });

        return comentario;
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Comentario GetComentario(int id)
    {
        var sql = "SELECT * FROM Comentario " +
                  "WHERE IdComentario = @IdComentario";

        return _bd.Query<Comentario>(sql, new { IdComentario = id }).Single();
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public List<Comentario> GetComentarios()
    {
        var sql = "SELECT * FROM Comentario";

        return _bd.Query<Comentario>(sql).ToList();
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    //Obtener comentario con artículo (Relación de uno a muchos)
    public List<Comentario> GetComentarioArticulo()
    {
        var sql = "SELECT c.*, a.Titulo " +
                  "FROM Comentario c " +
                  "INNER JOIN Articulo a ON c.ArticuloId = a.IdArticulo " +
                  "ORDER BY IdComentario DESC";

        var comentario = _bd.Query<Comentario, Articulo, Comentario>(sql, (c, a) => {
            c.Articulo = a;

            return c;
        }, splitOn: "ArticuloId");

        return comentario.Distinct().ToList();
    }
}
