using eeduca_api.Database;
using eeduca_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eeduca_api.Controllers
{
    public class BaseController : ApiController
    {
        protected Usuario ObterUsuario(string email, MySQLContext contexto = null)
        {
            MySQLContext ctx;
            if (contexto == null)
                ctx = new MySQLContext();
            else
                ctx = contexto;

            return ctx.Usuarios
                    .Where(u => u.Email == email)
                    .FirstOrDefault();
        }
    }
}
