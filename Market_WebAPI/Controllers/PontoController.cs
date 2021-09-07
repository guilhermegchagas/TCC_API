using Market_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Market_WebAPI.Controllers
{
    [EnableCors(origins: "http://reciclado-001-site1.etempurl.com", headers: "*", methods: "*")]

    public class RegisterPontoData
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoUsuario { get; set; }
    }

    public class PontoController : ApiController
    {
        public HttpResponseMessage PostPonto([FromBody]Ponto ponto)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.CadastrarPonto(ponto);
                    return Request.CreateResponse(HttpStatusCode.Created, "Ponto cadastrado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }

        public List<Ponto> GetPontosPorCodigoUsuario(int codigoUsuario)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(codigoUsuario);
                if (tokenStore.token == token)
                {
                    return DatabaseAcess.BuscarPontosPorCodigoUsuario(codigoUsuario);
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