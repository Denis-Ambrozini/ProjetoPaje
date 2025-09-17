using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoPaje.Models;

namespace ProjetoPaje.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            HomeModel home = new HomeModel();

            home.Nome = "Denis";
            return View(home);
        }

        public IActionResult Produtos()
        {
            return View();
        }
        public IActionResult Movimentacoes()
        {
            return View();
        }
        public IActionResult Fornecedores()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
