using Microsoft.AspNetCore.Mvc;

namespace ProjetoPaje.Controllers
{
    public class MovimentacoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
