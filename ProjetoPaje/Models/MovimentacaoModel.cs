using static ProjetoPaje.Models.ProdutoModel;
using static ProjetoPaje.Models.UsuarioModel;

namespace ProjetoPaje.Models
{
    public class MovimentacaoModel
    {
            public int Id { get; set; }
            public DateTime Data { get; set; } = DateTime.Now;
            public string Tipo { get; set; } // "Entrada", "Saída", "Ajuste"
            public int Quantidade { get; set; }
            public string Observacao { get; set; }

            // Relacionamentos
            public int ProdutoId { get; set; }
            public ProdutoModel Produto { get; set; }

            public int UsuarioId { get; set; }
            public UsuarioModel Usuario { get; set; }
        }

    
}
