
using Market_WebAPI.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Market_WebAPI.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                string email = context.UserName;
                string senha = context.Password;
                Usuario usuario = DatabaseAcess.BuscarUsuarioPorEmail(email);
                if(usuario != null)
                {
                    if(usuario.Senha == senha)
                    {
                        usuario.Senha = string.Empty;
                        var claims = new List<Claim>()
                        {
                            new Claim("ID", usuario.ID.ToString()),
                            new Claim(ClaimTypes.Name, usuario.Nome)
                        };

                        ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                        context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                    }
                    else
                    {
                        context.SetError("Senha Incorreta", "A senha digitada está incorreta.");
                    }   
                }
                else
                {
                    context.SetError("Email Inválido", "Nenhum usuário encontrado com este email.");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            string id = context.Identity.Claims.First().Value;
            var dataFile = "c:\\temp\\" + id + ".txt";
            //var dataFile = "h:\\root\\home\\reciclado-001\\www\\reciclado\\" + cpf + ".txt";
            TokenStore data = new TokenStore() { token = context.AccessToken, ID = id};
            File.WriteAllText(@dataFile, JsonConvert.SerializeObject(data));
            return base.TokenEndpointResponse(context);
        }
    }
}