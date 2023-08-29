using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers;

[Area("Admin")]
public class EtiquetasController : Controller
{
    private readonly IEtiquetaRepositorio _repoEtiqueta;

    public EtiquetasController(IEtiquetaRepositorio repoEtiqueta)
    {
        _repoEtiqueta = repoEtiqueta;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear([Bind("IdEtiqueta, NombreEtiqueta, FechaCreacion")] Etiqueta etiqueta)
    {
        if (ModelState.IsValid)
        {
            _repoEtiqueta.CrearEtiqueta(etiqueta);

            return RedirectToAction(nameof(Index));
        }

        return View(etiqueta);
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var etiqueta = _repoEtiqueta.GetEtiqueta(id.GetValueOrDefault());

        if (etiqueta == null)
        {
            return NotFound();
        }

        return View(etiqueta);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, [Bind("IdEtiqueta, NombreEtiqueta, FechaCreacion")] Etiqueta etiqueta)
    {
        if (id != etiqueta.IdEtiqueta)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _repoEtiqueta.ActualizarEtiqueta(etiqueta);

            return RedirectToAction(nameof(Index));
        }

        return View(etiqueta);
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpGet]
    public IActionResult GetArticuloConEtiquetas()
    {
        return View();
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    #region
    [HttpGet]
    public IActionResult GetEtiquetas()
    {
        return Json(new { data = _repoEtiqueta.GetEtiquetas() });
    }

    public IActionResult GetArticulosEtiquetas()
    {
        return Json(new { data = _repoEtiqueta.GetArticuloEtiquetas() });
    }

    [HttpDelete]
    public IActionResult BorrarEtiqueta(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            _repoEtiqueta.BorrarEtiqueta(id.GetValueOrDefault());

            return Json(new { success = true, message = "Etiqueta borrada correctamente" });
        }
    }
    #endregion
}
