using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Data
{
    public class Context : DbContext
    {
        private const string ConnectionString = @"Data Source=tcp:cadastrodeclientesdbserver.database.windows.net,1433;Initial Catalog=CadastroDeClientes_db;User Id=Arthur@cadastrodeclientesdbserver;Password=@rthur030701A";
        /// <summary>
        /// Server=tcp:cadastrodeclientesdbserver.database.windows.net,1433;Initial Catalog=CadastroDeClientes_db;Persist Security Info=False;User ID=Arthur;Password={@rthur030701A};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        /// </summary>
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: ConnectionString);
        }
    }
}
