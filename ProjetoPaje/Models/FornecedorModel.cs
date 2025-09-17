namespace ProjetoPaje.Models
{
    public class FornecedorModel
    {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Cnpj { get; set; }
            public string Telefone { get; set; }
            public string Email { get; set; }
            public string Endereco { get; set; }

            //public ICollection<Produto> Produtos { get; set; }
        }

    
}
