using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoPaje.Models
{
    public class MovimentacaoModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O tipo da movimentação é obrigatório.")]
        public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saída"

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        // Relacionamento com Produto
        [Required]
        public int ProdutoId { get; set; }
        public virtual ProdutoModel Produto { get; set; } = null!;
    }
}

