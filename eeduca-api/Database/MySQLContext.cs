using eeduca_api.Models;
using MySql.Data.Entity;
using System.Data.Entity;

namespace eeduca_api.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLContext: DbContext
    {
        private static MySQLContext instancia = null;
        private static readonly object Trava = new object();

        public static MySQLContext ObterInstancia
        {
            get
            {
                if (instancia == null)
                {
                    lock(Trava)
                    {
                        if (instancia == null)
                        {
                            instancia = new MySQLContext();
                        }
                    }                    
                }
                return instancia;
            }
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }

        public MySQLContext(): base("name=ConexaoMySQL")
        {
        }
    }
}