using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(APIProyectoSC_601.Startup))]

namespace APIProyectoSC_601
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuthTokenGeneration(app);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            var issuer = "your_issuer_url"; // URL del emisor del token
            var audience = "your_audience_url"; // URL del destinatario del token
            var secret = ConfigurationManager.AppSettings["SecretKey"];
            var secretBytes = TextEncodings.Base64Url.Decode(secret);

            // Configurar parámetros de validación del token JWT
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = true, // Validar el tiempo de vida del token

                LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                {
                    if (expires != null)
                    {
                        return expires > DateTime.UtcNow;
                    }
                    return false;
                }
            };

            // Utilizar el middleware de autenticación JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = tokenValidationParameters // Establecer los parámetros de validación
                });
        }
    }
}
