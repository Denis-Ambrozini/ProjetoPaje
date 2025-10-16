using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPaje.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPaje.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly BancoContext _context;

        public RelatoriosController(BancoContext context)
        {
            _context = context;
        }

        // GET: Relatorios
        public IActionResult Index()
        {
            return View();
        }

        // GET: Relatorios/EstoqueBaixo
        public async Task<IActionResult> EstoqueBaixo()
        {
            var produtosBaixoEstoque = await _context.Produtos
                .Where(p => p.Quantidade <= p.QuantidadeMinima)
                .ToListAsync();
            return View(produtosBaixoEstoque);
        }

        // GET: Relatorios/HistoricoMovimentacoes
        public async Task<IActionResult> HistoricoMovimentacoes(string produtoNome, DateTime? dataInicio, DateTime? dataFim)
        {
            var query = _context.Movimentacoes.Include(m => m.Produto).AsQueryable();

            if (!string.IsNullOrEmpty(produtoNome))
            {
                query = query.Where(m => m.Produto.Nome.Contains(produtoNome));
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(m => m.Data >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                query = query.Where(m => m.Data <= dataFim.Value);
            }

            var movimentacoes = await query.OrderByDescending(m => m.Data).ToListAsync();
            ViewData["ProdutoNomeFilter"] = produtoNome;
            ViewData["DataInicioFilter"] = dataInicio?.ToString("yyyy-MM-dd");
            ViewData["DataFimFilter"] = dataFim?.ToString("yyyy-MM-dd");

            return View(movimentacoes);
        }
    }
}

