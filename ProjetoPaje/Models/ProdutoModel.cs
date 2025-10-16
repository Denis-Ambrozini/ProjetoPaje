using System.ComponentModel.DataAnnotations;

namespace ProjetoPaje.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "A quantidade atual é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser um número positivo.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "A quantidade mínima é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade mínima deve ser maior que zero.")]
        public int QuantidadeMinima { get; set; }

        public virtual ICollection<MovimentacaoModel> Movimentacoes { get; set; }
    }
}

