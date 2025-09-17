namespace ProjetoPaje.Models
{
    public class UsuarioModel
    {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string SenhaHash { get; set; } // senha deve ser armazenada com hash
            public string Role { get; set; } // "Admin", "Operador", "Consulta"

            // public ICollection<Movimentacao> Movimentacoes { get; set; } // histórico
        }

    
}
