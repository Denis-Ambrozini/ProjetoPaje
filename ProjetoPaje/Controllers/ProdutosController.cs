using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPaje.Data;
using ProjetoPaje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPaje.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly BancoContext _context;

        public ProdutosController(BancoContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produtos/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos
                .Include(p => p.Movimentacoes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // GET: Produtos/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Produtos/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Id,Nome,Categoria,Quantidade,QuantidadeMinima")] ProdutoModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtoModel);
        }

        // GET: Produtos/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos.FindAsync(id);
            if (produtoModel == null)
            {
                return NotFound();
            }
            return View(produtoModel);
        }

        // POST: Produtos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Nome,Categoria,QuantidadeMinima")] ProdutoModel produtoModel)
        {
            if (id != produtoModel.Id)
            {
                return NotFound();
            }

            // Recupera o produto existente para manter a quantidade atual
            var produtoExistente = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            // Atualiza apenas os campos permitidos
            produtoExistente.Nome = produtoModel.Nome;
            produtoExistente.Categoria = produtoModel.Categoria;
            produtoExistente.QuantidadeMinima = produtoModel.QuantidadeMinima;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoModelExists(produtoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtoModel);
        }

        // GET: Produtos/Apagar/5
        public async Task<IActionResult> Apagar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // POST: Produtos/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarApagar(int id)
        {
            var produtoModel = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoModelExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}

