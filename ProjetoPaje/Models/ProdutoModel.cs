using static ProjetoPaje.Models.CategoriaModel;
using static ProjetoPaje.Models.FornecedorModel;

namespace ProjetoPaje.Models
{
    public class ProdutoModel
    {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string CodigoBarras { get; set; }
            public int Quantidade { get; set; }
            public decimal PrecoCompra { get; set; }
            public decimal PrecoVenda { get; set; }
            public int EstoqueMinimo { get; set; }
            public DateTime? Validade { get; set; }
            public string Localizacao { get; set; } // prateleira/corredor
            public bool Obsoleto { get; set; } // se já não deve mais ser comprado

            // Relacionamentos
            public int CategoriaId { get; set; }
            public CategoriaModel Categoria { get; set; }

            public int FornecedorId { get; set; }
            public FornecedorModel Fornecedor { get; set; }

            //public ICollection<Movimentacao> Movimentacoes { get; set; }
        }
 }
