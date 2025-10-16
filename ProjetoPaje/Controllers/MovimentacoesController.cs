using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPaje.Data;
using ProjetoPaje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPaje.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly BancoContext _context;

        public MovimentacoesController(BancoContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var movimentacoes = _context.Movimentacoes.Include(m => m.Produto).OrderByDescending(m => m.Data);
            return View(await movimentacoes.ToListAsync());
        }

        // GET: Movimentacoes/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoModel = await _context.Movimentacoes
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacaoModel == null)
            {
                return NotFound();
            }

            return View(movimentacaoModel);
        }

        // GET: Movimentacoes/Criar
        public IActionResult Criar()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            return View();
        }

        // POST: Movimentacoes/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Tipo,Quantidade,ProdutoId")] MovimentacaoModel movimentacaoModel)
        {
            movimentacaoModel.Data = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                var produto = await _context.Produtos.FindAsync(movimentacaoModel.ProdutoId);
                if (produto == null)
                {
                    ModelState.AddModelError("ProdutoId", "Produto não encontrado.");
                }
                else
                {
                    if (movimentacaoModel.Tipo == "Entrada")
                    {
                        produto.Quantidade += movimentacaoModel.Quantidade;
                    }
                    else if (movimentacaoModel.Tipo == "Saída")
                    {
                        if (produto.Quantidade < movimentacaoModel.Quantidade)
                        {
                            ModelState.AddModelError("Quantidade", "Quantidade em estoque insuficiente para esta saída.");
                        }
                        else
                        {
                            produto.Quantidade -= movimentacaoModel.Quantidade;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Tipo", "Tipo de movimentação inválido. Use 'Entrada' ou 'Saída'.");
                    }
                }

                if (ModelState.ErrorCount == 0)
                {
                    _context.Add(movimentacaoModel);
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", movimentacaoModel.ProdutoId);
            return View(movimentacaoModel);
        }

        private bool MovimentacaoModelExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }
    }
}

