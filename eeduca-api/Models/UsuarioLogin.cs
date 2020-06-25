using eeduca_api.Classes;
using eeduca_api.Database;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace eeduca_api.Models
{
    public class UsuarioLogin
    {        
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        
        [JsonIgnore]
        public int Id { get; set; }

        public bool ValidarLogin()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Senha))
                return false;

            Usuario usuario = new MySQLContext().Usuarios
                                    .Where(u => u.Email == Email)
                                    .FirstOrDefault();

            if (usuario == null)
                return false;

            Id = usuario.Id;
            return Crypto.CompararHashes(usuario.Senha, Crypto.CalcularHash(Senha));
        }
    }
}