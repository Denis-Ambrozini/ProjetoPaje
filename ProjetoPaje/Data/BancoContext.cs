// ----------------- IMPORTS NECESSÁRIOS -----------------
// Para o DbContext e UseSqlServer
using Microsoft.EntityFrameworkCore;
// Para ler o appsettings.json (IConfigurationRoot, ConfigurationBuilder)
using Microsoft.Extensions.Configuration;
// Para encontrar o caminho do projeto (Directory.GetCurrentDirectory)
using System.IO;
// Para seus modelos (ex: ProdutoModel)
using ProjetoPaje.Models;


// ----------------- NAMESPACE DO SEU PROJETO -----------------
namespace ProjetoPaje.Data
{
    /// <summary>
    /// Classe que representa a sessão com o banco de dados e permite
    /// consultar e salvar instâncias de suas entidades.
    /// </summary>
    public class BancoContext : DbContext
    {
        // ----------------- CONSTRUTORES -----------------

        /// <summary>
        /// Construtor usado pela Injeção de Dependência do ASP.NET Core quando a aplicação está em execução.
        /// Ele recebe as opções configuradas no Program.cs.
        /// </summary>
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        /// <summary>
        /// Construtor vazio. Necessário para que as ferramentas de design do Entity Framework (como Add-Migration)
        /// possam criar uma instância da classe.
        /// </summary>
        public BancoContext()
        {
        }


        // ----------------- MÉTODO DE CONFIGURAÇÃO DE "TEMPO DE DESIGN" -----------------

        /// <summary>
        /// Este método é chamado pelas ferramentas de linha de comando (Update-Database, etc.).
        /// Ele serve como um "Plano B" para configurar a conexão quando o Program.cs não está sendo executado.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verifica se o DbContext já não foi configurado em outro lugar (ex: no Program.cs)
            if (!optionsBuilder.IsConfigured)
            {
                // 1. Cria um construtor de configuração
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   // 2. Define o caminho base como o diretório atual do projeto
                   .SetBasePath(Directory.GetCurrentDirectory())
                   // 3. Adiciona o nosso arquivo appsettings.json como fonte
                   .AddJsonFile("appsettings.json")
                   // 4. Constrói a configuração
                   .Build();

                // 5. Pega a string de conexão específica que precisamos (pelo nome "DataBase")
                var connectionString = configuration.GetConnectionString("DataBase");

                // 6. Configura o DbContext para usar o SQL Server com a string de conexão obtida
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        // ----------------- MAPEAMENTO DAS TABELAS -----------------

        /// <summary>
        /// Mapeia a classe ProdutoModel para uma tabela chamada "Produtos" no banco de dados.
        /// </summary>
        public DbSet<ProdutoModel> Produtos { get; set; }

        // Adicione outros DbSets para outras tabelas aqui...
        // public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}