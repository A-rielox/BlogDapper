using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriasController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
