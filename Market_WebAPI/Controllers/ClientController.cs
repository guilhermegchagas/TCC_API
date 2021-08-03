using Market_WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Market_WebAPI.Controllers
{
    [EnableCors(origins: "http://reciclado-001-site1.etempurl.com", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        public Cliente GetClientInfo(string cpf)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(cpf);
                if (tokenStore.token == token)
                {
                    Cliente cliente = DatabaseAcess.BuscarClientePorCPF(cpf);
                    cliente.Senha = string.Empty;
                    return cliente;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
