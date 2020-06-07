using eeduca_api.Models;
using eeduca_api.Database;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace eeduca_api.Controllers
{
    [Authorize]
    public class GruposController : BaseController
    {
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
                MySQLContext contexto = new MySQLContext();

                NovoGrupo = new Grupo
                {
                    Nome = Nome,
                    Descricao = Descricao,
                    UsuarioId = ObterUsuario(RequestContext.Principal.Identity.Name, contexto).Id
                };                

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
            return new MySQLContext().Grupos
                        .Where(g => g.Id == Id)
                        .FirstOrDefault();
        }

        [Route("api/Grupos/Listar")]
        [HttpGet]
        public List<Grupo> Listar()
        {
            MySQLContext contexto = new MySQLContext();
            Usuario usuario = ObterUsuario(RequestContext.Principal.Identity.Name, contexto);

            return contexto.Grupos
                        .Where(g => g.UsuarioId == usuario.Id)
                        .ToList();
        }

        [Route("api/Grupos/ObterChaveIngresso")]
        [HttpGet]
        public string ObterChaveIngresso(int Id)
        {
            MySQLContext contexto = new MySQLContext();
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

        [Route("api/Grupos/ListarMensagens")]
        [HttpGet]
        public List<GrupoMensagem> ListarMensagens(int GrupoId)
        {
            return new MySQLContext().GrupoMensagens
                        .Where(gm => gm.GrupoId == GrupoId)
                        .OrderBy(gm => gm.DataHoraCriacao)
                        .ToList();
        }

        [Route("api/Grupos/PostarMensagem")]
        [HttpPost]
        public HttpResponseMessage PostarMensagem(int GrupoId, string Mensagem)
        {
            HttpResponseMessage retorno = new HttpResponseMessage();
            GrupoMensagem NovaMensagem;
            MySQLContext contexto = new MySQLContext();
            Usuario usuario = ObterUsuario(RequestContext.Principal.Identity.Name, contexto);
            Grupo grupo = contexto.Grupos.FirstOrDefault(g => g.Id == GrupoId);

            if (String.IsNullOrWhiteSpace(Mensagem))
            {
                retorno.ReasonPhrase = "Você não pode postar uma mensagem vazia!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }         
            
            if (grupo == null)
            {
                retorno.ReasonPhrase = "O grupo indicado não foi encontrado!";
                retorno.StatusCode = HttpStatusCode.BadRequest;
                return retorno;
            }

            try
            {
                NovaMensagem = new GrupoMensagem
                {
                    Usuario = usuario,
                    Grupo = grupo,
                    Mensagem = Mensagem
                };

                contexto.GrupoMensagens.Add(NovaMensagem);
                contexto.SaveChanges();
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;

                retorno.Content = new StringContent(e.Message + "\n" + e.StackTrace);
                retorno.StatusCode = HttpStatusCode.InternalServerError;
                return retorno;
            }

            retorno.ReasonPhrase = "Mensagem postada com sucesso!";
            retorno.StatusCode = HttpStatusCode.OK;
            retorno.Headers.Add("MensagemId", NovaMensagem.Id.ToString());
            return retorno;
        }
    }
}