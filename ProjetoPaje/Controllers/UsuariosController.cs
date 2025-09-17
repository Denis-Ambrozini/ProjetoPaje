using Microsoft.AspNetCore.Mvc;

namespace ProjetoPaje.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
