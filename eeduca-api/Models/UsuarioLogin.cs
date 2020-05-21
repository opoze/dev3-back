namespace eeduca_api.Models
{
    public class UsuarioLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool ValidarLogin()
        {
            //@Lucas TODO: Implementar um login de verdade
            return Email.Equals("lucas.rtk@hotmail.com") && Senha.Equals("1");
        }
    }
}