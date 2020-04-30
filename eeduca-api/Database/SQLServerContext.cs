using eeduca_api.Models;
using System.Data.Entity;

namespace eeduca_api.Database
{
    public class SQLServerContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }

        public SQLServerContext() : base("ConexaoAzure")
        {
        }
    }
}