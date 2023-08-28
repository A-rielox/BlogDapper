using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers;

//[Authorize]
[Area("Admin")]
public class ArticulosController : Controller
{
    private readonly ICategoriaRepositorio _repoCategoria;
    private readonly IArticuloRepositorio _repoArticulo;
    //private readonly IEtiquetaRepositorio _repoEtiqueta;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ArticulosController(ICategoriaRepositorio repoCategoria,
                               IArticuloRepositorio repoArticulo,
                               //IEtiquetaRepositorio repoEtiqueta,
                               IWebHostEnvironment hostingEnvironment)
    {
        _repoCategoria = repoCategoria;
        _repoArticulo = repoArticulo;
        _hostingEnvironment = hostingEnvironment;
        //_repoEtiqueta = repoEtiqueta;
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
        ViewBag.SelectList = _repoCategoria.GetListaCategorias();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear([Bind("IdArticulo, Titulo, Descripcion, Imagen, Estado, CategoriaId, FechaCreacion")] Articulo articulo)
    {
        if (ModelState.IsValid)
        {
            string rutaPrincipal = _hostingEnvironment.WebRootPath; // acceso a carpeta wwwroot
            var archivos = HttpContext.Request.Form.Files;

            if (articulo.IdArticulo == 0)
            {
                //Nuevo artículo
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                articulo.Imagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                _repoArticulo.CrearArticulo(articulo);
                return RedirectToAction(nameof(Index));
            }

            //Esta línea valida el modelo si es "false" retorna a la vista crear pero del GET, o sea al formulario               
            return RedirectToAction(nameof(Crear));
        }
        return View(articulo);
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

        var articulo = _repoArticulo.GetArticulo(id.GetValueOrDefault());

        if (articulo == null)
        {
            return NotFound();
        }

        ViewBag.SelectList = _repoCategoria.GetListaCategorias();
        return View(articulo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, [Bind("IdArticulo, Titulo, Descripcion, Imagen, Estado, CategoriaId, FechaCreacion")] Articulo articulo)
    {
        if (ModelState.IsValid)
        {
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            var articuloDesdeDb = _repoArticulo.GetArticulo(id);


            if (archivos.Count() > 0)
            {
                //Editamos o cambiamos la imagen del artículo
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                var extension = Path.GetExtension(archivos[0].FileName);
                var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.Imagen.TrimStart('\\'));

                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }

                //Subimos el nuevo archivo
                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                articulo.Imagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtension;

                _repoArticulo.ActualizarArticulo(articulo);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //Aquí es cuando la imagen ya existe y no se reemplaza
                articulo.Imagen = articuloDesdeDb.Imagen;
            }

            _repoArticulo.ActualizarArticulo(articulo);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Editar));
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    //Para la parte de asignar etiquetas a un artículo
    //[HttpGet]
    //public IActionResult AsignarEtiquetas(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var articulo = _repoArticulo.GetArticulo(id.GetValueOrDefault());
    //    if (articulo == null)
    //    {
    //        return NotFound();
    //    }

    //    ViewBag.SelectList = _repoEtiqueta.GetListaEtiquetas();
    //    return View(articulo);
    //}


    //////////////////////////////////////////
    /////////////////////////////////////////////
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult AsignarEtiquetaArticulo(int IdArticulo, int IdEtiqueta)
    //{
    //    if (IdArticulo == 0 || IdEtiqueta == 0)
    //    {
    //        ViewBag.SelectList = _repoEtiqueta.GetListaEtiquetas();
    //        return View();
    //    }
    //    else
    //    {
    //        ArticuloEtiquetas artiEtiquetas = new ArticuloEtiquetas();
    //        artiEtiquetas.IdArticulo = IdArticulo;
    //        artiEtiquetas.IdEtiqueta = IdEtiqueta;

    //        _repoEtiqueta.AsignarEtiquetas(artiEtiquetas);
    //        return RedirectToAction(nameof(Index));
    //    }
    //}



    //////////////////////////////////////////
    /////////////////////////////////////////////
    #region
    [HttpGet]
    public IActionResult GetArticulos()
    {
        return Json(new { data = _repoArticulo.GetArticuloCategoria() });
    }

    [HttpDelete]
    public IActionResult BorrarArticulo(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        else
        {
            _repoArticulo.BorrarArticulo(id.GetValueOrDefault());

            return Json(new { success = true, message = "Articulo borrado correctamente" });
        }
    }
    #endregion
}
