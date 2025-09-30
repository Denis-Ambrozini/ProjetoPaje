using Microsoft.EntityFrameworkCore;
using ProjetoPaje.Models;

namespace ProjetoPaje.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
    }
}
