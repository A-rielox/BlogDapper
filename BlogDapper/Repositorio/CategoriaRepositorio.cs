using BlogDapper.Models;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlogDapper.Repositorio;

public class CategoriaRepositorio : ICategoriaRepositorio
{
    private readonly IDbConnection _bd;
    public CategoriaRepositorio(IConfiguration configuration)
    {
        _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Categoria ActualizarCategoria(Categoria categoria)
    {
        var sql = "UPDATE Categoria SET Nombre = @Nombre WHERE IdCategoria = @IdCategoria";

        _bd.Execute(sql, categoria);

        return categoria;
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public void BorrarCategoria(int id)
    {
        var sql = "DELETE FROM Categoria WHERE IdCategoria = @IdCategoria";

        _bd.Execute(sql, new { IdCategoria = id });
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Categoria CrearCategoria(Categoria categoria)
    {
        var sql = "INSERT INTO Categoria(Nombre, FechaCreacion)" +
                  " VALUES(@Nombre, @FechaCreacion)";

        _bd.Execute(sql, new
        {
            categoria.Nombre,
            FechaCreacion = DateTime.Now
        });

        return categoria;
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public Categoria GetCategoria(int id)
    {
        var sql = "SELECT * FROM Categoria WHERE IdCategoria = @IdCategoria";

        return _bd.Query<Categoria>(sql, new { IdCategoria = id }).Single();
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    public List<Categoria> GetCategorias()
    {
        var sql = "SELECT * FROM Categoria";

        return _bd.Query<Categoria>(sql).ToList();
    }
}
