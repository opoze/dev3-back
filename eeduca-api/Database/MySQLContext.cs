using eeduca_api.Models;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace eeduca_api.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }

        public MySQLContext() : base("ConexaoMySQL")
        {
        }
    }
}