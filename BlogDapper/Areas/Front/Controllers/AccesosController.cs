using BlogDapper.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using Dapper;
using XSystem.Security.Cryptography;

namespace BlogDapper.Areas.Front.Controllers;

[Area("Front")]
public class AccesosController : Controller
{
    private readonly IDbConnection _bd;

    public AccesosController(IConfiguration configuration)
    {
        _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
    }



    //////////////////////////////////////////
    /////////////////////////////////////////////
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Acceso()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UserLogin(Usuario user)
    {
        if (ModelState.IsValid)
        {
            var sql = "SELECT * FROM Usuario WHERE Login = @Login AND Password = @Password";

            // Encriptar password a md5  antes de enviar la consulta
            var Password = obtenermd5(user.Password);

            var validar = _bd.Query<Usuario>(sql, new
            {
                user.Login,
                Password
            });

            if (validar.Count() == 1)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                TempData["mensajeConfirmacion"] = "Datos de acceso incorrectos";

                return RedirectToAction("Acceso", "Accesos");
            }

        }
        else
        {
            TempData["mensajeConfirmacion"] = "Algunos campos obligatorios están vacíos";

            return RedirectToAction("Acceso", "Accesos");
        }
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UserRegistro(Usuario user)
    {
        if (ModelState.IsValid)
        {
            var sql = "SELECT * FROM Usuario WHERE Login = @Login";

            var existeUsuario = _bd.Query<Usuario>(sql, new
            {
                user.Login
            });

            if (existeUsuario.Count() > 0)
            {
                TempData["mensajeConfirmacion"] = "El usuario ya existe";

                return RedirectToAction("Registro", "Accesos");
            }
            else
            {
                // Encriptar password a md5  antes de enviar la consulta
                var Password = obtenermd5(user.Password);

                var User_ID = Guid.NewGuid();

                var ingresarUsuarioSql = "insert into Usuario(User_ID, Login, Password) VALUES(@User_ID, @Login, @Password)";

                _bd.Execute(ingresarUsuarioSql, new
                {
                    User_ID,
                    user.Login,
                    Password
                });

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Inicio");
            }
        }
        else
        {
            TempData["mensajeConfirmacion"] = "Algunos campos obligatorios están vacíos";

            return RedirectToAction("Registro", "Accesos");
        }
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Salir()
    {
        HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Inicio");
    }


    //////////////////////////////////////////
    /////////////////////////////////////////////
    //Método para encriptar contraseña con MD5 se usa tanto en el Acceso como en el Registro
    public static string obtenermd5(string valor)
    {
        // MD5CryptoServiceProvider necesita XAct.Core.PCL
        MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();

        byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);

        data = x.ComputeHash(data);

        string resp = "";

        for (int i = 0; i < data.Length; i++)
            resp += data[i].ToString("x2").ToLower();

        return resp;
    }
}
