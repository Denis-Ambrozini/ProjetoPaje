using Microsoft.AspNetCore.Mvc;

namespace ProjetoPaje.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Apagar()
        {
            return View();
        }
    }
}
