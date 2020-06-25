using eeduca_api.Database;
using eeduca_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace eeduca_api.Controllers
{
    public class BaseController : ApiController
    {
        protected Usuario ObterUsuario(ClaimsIdentity ClaimsIdentity, MySQLContext Contexto = null)
        {
            string Email = "";
            foreach (var claim in ClaimsIdentity.Claims)
            {
                if (claim.Type == ClaimTypes.Email)
                {
                    Email = claim.Value;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(Email))
                return null;

            MySQLContext ctx;
            if (Contexto == null)
                ctx = new MySQLContext();
            else
                ctx = Contexto;

            return ctx.Usuarios
                    .Where(u => u.Email == Email)
                    .FirstOrDefault();
        }

        protected Usuario ObterUsuario(string Email, MySQLContext Contexto = null)
        {
            MySQLContext ctx;
            if (Contexto == null)
                ctx = new MySQLContext();
            else
                ctx = Contexto;

            return ctx.Usuarios
                    .Where(u => u.Email == Email)
                    .FirstOrDefault();
        }
    }
}
