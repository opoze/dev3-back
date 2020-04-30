using eeduca_api.Models;
using eeduca_api.Database;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace eeduca_api.Controllers
{
    public class GruposController : ApiController
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //CÓDIGO TEMPORÁRIO!!!! REMOVER NO FUTURO
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private Usuario ObterUsuarioDeTestes()
        {
            SQLServerContext contexto = new SQLServerContext();
            Usuario usuario = contexto.Usuarios
                                .Where(u => u.Email == "lucas.rtk@hotmail.com")
                                .FirstOrDefault();

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Email = "lucas.rtk@hotmail.com",
                    Nome = "Lucas Rutkoski",
                    Senha = Convert.FromBase64String("273a0bfec91744347e874922ad05f4380e401937508ebb28") //Senha: 1
                };

                contexto.Usuarios.Add(usuario);
                contexto.SaveChanges();
            }

            return usuario;
        }

        [Route("api/Grupos/Novo")]
        [HttpPost]
        public HttpResponseMessage Novo(string Nome, string Descricao = null)
        {
            HttpResponseMessage retorno = new HttpResponseMessage();
            Grupo NovoGrupo;

            if (String.IsNullOrWhiteSpace(Nome))
            {
                retorno.ReasonPhrase = "O nome do grupo não pode estar em branco!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }

            try
            {
                NovoGrupo = new Grupo
                {
                    Nome = Nome,
                    Descricao = Descricao,
                    UsuarioId = ObterUsuarioDeTestes().Id
                };

                SQLServerContext contexto = new SQLServerContext();

                contexto.Grupos.Add(NovoGrupo);
                contexto.SaveChanges();
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

        [Route("api/Grupos/Obter")]
        [HttpGet]
        public Grupo Obter(int Id)
        {
            return new SQLServerContext().Grupos
                        .Where(g => g.Id == Id)
                        .FirstOrDefault();
        }

        [Route("api/Grupos/Listar")]
        [HttpGet]
        public List<Grupo> Listar()
        {
            Usuario usuario = ObterUsuarioDeTestes();

            return new SQLServerContext().Grupos
                        .Where(g => g.UsuarioId == usuario.Id)
                        .ToList();
        }

        [Route("api/Grupos/ObterChaveIngresso")]
        [HttpGet]
        public string ObterChaveIngresso(int Id)
        {
            SQLServerContext contexto = new SQLServerContext();
            Grupo grupo = contexto.Grupos
                        .Where(g => g.Id == Id)
                        .FirstOrDefault();

            if (grupo == null)
                return null;

            if (String.IsNullOrWhiteSpace(grupo.Chave))
            {
                grupo.GerarChaveIngresso();
                contexto.SaveChanges();
            }

            return grupo.Chave;
        }
    }
}