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
        private const string ConnectionString = @"Data Source=DESKTOP-ARTKF28\SQLEXPRESS;Initial Catalog=Desenvolvimento;Integrated Security=True";

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: ConnectionString);
        }
    }
}
