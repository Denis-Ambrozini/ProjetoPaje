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
        public DbSet<MovimentacaoModel> Movimentacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura o relacionamento entre Produto e Movimentacao
            modelBuilder.Entity<MovimentacaoModel>()
                .HasOne(m => m.Produto)
                .WithMany(p => p.Movimentacoes)
                .HasForeignKey(m => m.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

