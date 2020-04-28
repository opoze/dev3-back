using eeduca_api.Models;
using eeduca_api.Database;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace eeduca_api.Controllers
{
    public class GruposController : ApiController
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //CÓDIGO TEMPORÁRIO!!!! REMOVER NO FUTURO
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private Usuario ObterUsuarioDeTestes()
        {
            Usuario usuario = MySQLContext.ObterInstancia.Usuarios
                                                         .Where(u => u.Email.Equals("lucas.rtk@hotmail.com"))
                                                         .FirstOrDefault();

            if (usuario == null) 
            {
                MySQLContext.ObterInstancia.Usuarios.Add(new Models.Usuario
                {
                    Email = "lucas.rtk@hotmail.com",
                    Nome = "Lucas Rutkoski",
                    Senha = Convert.FromBase64String("273a0bfec91744347e874922ad05f4380e401937508ebb28") //Senha: 1
                });
                MySQLContext.ObterInstancia.SaveChanges();
            }

            return usuario;
        }

        [Route("api/Grupos/CriarGrupo")]
        [HttpPost]
        public HttpResponseMessage CriarGrupo(string NomeGrupo)
        {
            HttpResponseMessage retorno = new HttpResponseMessage();
            Grupo NovoGrupo;

            if (String.IsNullOrWhiteSpace(NomeGrupo))
            {
                retorno.ReasonPhrase = "O nome do grupo não pode estar em branco!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }

            try
            {
                NovoGrupo = new Grupo
                {
                    Nome = NomeGrupo,
                    Administrador = ObterUsuarioDeTestes()
                };

                MySQLContext.ObterInstancia.Grupos.Add(NovoGrupo);
                MySQLContext.ObterInstancia.SaveChanges();
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;

                retorno.Content = new StringContent(e.Message + "\n" + e.StackTrace);
                retorno.StatusCode = HttpStatusCode.InternalServerError;
                return retorno;
            }

            retorno.ReasonPhrase = "Grupo criado com sucesso!";
            retorno.StatusCode = HttpStatusCode.OK;
            retorno.Headers.Add("GrupoId", NovoGrupo.Id.ToString());
            return retorno;
        }

        [Route("api/Grupos/ObterGrupo")]
        [HttpGet]
        public Grupo ObterGrupo(int Id)
        {
            return MySQLContext.ObterInstancia.Grupos
                                              .Where(g => g.Id.Equals(Id))
                                              .FirstOrDefault();
        }

        [Route("api/Grupos/ObterChaveIngresso")]
        [HttpGet]
        public string ObterChaveIngresso(int Id)
        {
            Grupo grupo = ObterGrupo(Id);

            if (grupo == null)
                return null;

            if (String.IsNullOrWhiteSpace(grupo.Chave))
            {
                grupo.GerarChaveIngresso();
                MySQLContext.ObterInstancia.SaveChanges();
            }

            return grupo.Chave;
        }
    }
}