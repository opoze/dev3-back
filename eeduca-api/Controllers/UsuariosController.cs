using eeduca_api.Classes;
using eeduca_api.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eeduca_api.Controllers
{
    [AllowAnonymous]
    public class UsuariosController : ApiController
    {
        [Route("api/Usuarios/Login")]
        [HttpPost]        
        public HttpResponseMessage Login(UsuarioLogin usuario)
        {
            HttpResponseMessage retorno = new HttpResponseMessage();            

            if (!usuario.ValidarLogin())
            {
                retorno.ReasonPhrase = "O e-mail ou senha informados estão incorretos!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }

            string token = TokenManager.GerarToken(usuario.Email);

            retorno.ReasonPhrase = "Login efetuado!";
            retorno.StatusCode = HttpStatusCode.OK;
            retorno.Content = new StringContent(token);
            retorno.Headers.Add("token", token);

            return retorno;
        }

        [Route("api/Usuarios/ValidarToken")]
        [HttpGet]
        public HttpResponseMessage ValidarToken(string token, string email)
        {
            HttpResponseMessage retorno = new HttpResponseMessage();

            //@Lucas TODO: Implementar a validação no DbSet de usuários
            if (!true)
            {
                retorno.ReasonPhrase = "O usuário informado não foi encontrado!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }

            string tokenUsername = TokenManager.ValidarToken(token);
            if (email.Equals(tokenUsername))
            {
                retorno.ReasonPhrase = "Token validado.";
                retorno.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                retorno.ReasonPhrase = "Token inválido.";
                retorno.StatusCode = HttpStatusCode.BadRequest;
            }

            return retorno;
        }
    }
}