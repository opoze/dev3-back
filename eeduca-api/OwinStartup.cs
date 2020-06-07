using eeduca_api.Classes;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

[assembly: OwinStartup(typeof(eeduca_api.OwinStartup))]

namespace eeduca_api
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = TokenManager.ObterParametrosValidacao()
            });
        }
    }
}
