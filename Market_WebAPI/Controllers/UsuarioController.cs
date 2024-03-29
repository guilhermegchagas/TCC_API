﻿using Market_WebAPI.Models;
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
    public class UsuarioController : ApiController
    {
        public Usuario GetUsuarioInfo(string email)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                Usuario usuario = DatabaseAcess.BuscarUsuarioPorEmail(email);
                TokenStore tokenStore = TokenStore.GetTokenStore(usuario.ID);
                if (tokenStore.token == token)
                {
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
