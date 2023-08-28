using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers;

[Area("Admin")]
public class ComentariosController : Controller
{
    private readonly IComentarioRepositorio _repoComentario;

    public ComentariosController(IComentarioRepositorio repoComentario)
    {
        _repoComentario = repoComentario;
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    #region
    [HttpGet]
    public IActionResult GetComentarios()
    {
        return Json(new { data = _repoComentario.GetComentarioArticulo() });
    }

    [HttpDelete]
    public IActionResult BorrarComentario(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            _repoComentario.BorrarComentario(id.GetValueOrDefault());
            return Json(new { success = true, message = "Comentario borrado correctamente" });
        }
    }
    #endregion
}
