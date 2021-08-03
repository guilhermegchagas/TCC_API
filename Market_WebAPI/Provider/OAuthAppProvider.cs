
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
                var cpf = context.UserName;
                var senha = context.Password;
                Cliente cliente = DatabaseAcess.BuscarClientePorCPF(cpf);
                if(cliente != null)
                {
                    if(cliente.Senha == senha)
                    {
                        cliente.Senha = string.Empty;
                        var claims = new List<Claim>()
                        {
                            new Claim("CPF", cliente.CPF),
                            new Claim(ClaimTypes.Name, cliente.Nome)
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
                    context.SetError("CPF Inválido", "Nenhum cliente encontrado com este CPF.");
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
            string cpf = context.Identity.Claims.First().Value;
            var dataFile = "c:\\temp\\" + cpf + ".txt";
            //var dataFile = "h:\\root\\home\\reciclado-001\\www\\reciclado\\" + cpf + ".txt";
            TokenStore data = new TokenStore() { token = context.AccessToken, CPF = cpf};
            File.WriteAllText(@dataFile, JsonConvert.SerializeObject(data));
            return base.TokenEndpointResponse(context);
        }
    }
}