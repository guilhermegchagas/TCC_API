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
    public class UsuarioController : ApiController
    {
        public Usuario GetUsuarioInfo(int id)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(id);
                if (tokenStore.token == token)
                {
                    Usuario usuario = DatabaseAcess.BuscarUsuarioPorID(id);
                    usuario.Senha = string.Empty;
                    return usuario;
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
