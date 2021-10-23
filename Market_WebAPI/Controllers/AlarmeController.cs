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
    public class AlarmeController : ApiController
    {
        [Route("api/alarme/cadastrar")]
        public HttpResponseMessage PostAlarme([FromBody]Alarme alarme)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(alarme.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.CadastrarAlarme(alarme);
                    return Request.CreateResponse(HttpStatusCode.Created, "Alarme cadastrado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/alarme/atualizar")]
        public HttpResponseMessage PutAlarme([FromBody]Alarme alarme)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(alarme.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.AtualizarAlarme(alarme);
                    return Request.CreateResponse(HttpStatusCode.Created, "Alarme atualizado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/alarme/deletar")]
        public HttpResponseMessage DeletePonto([FromBody]Alarme alarme)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(alarme.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.DeletarAlarme(alarme);
                    return Request.CreateResponse(HttpStatusCode.Created, "Alarme deletado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }

        public List<Alarme> GetAlarmesPorPonto(int codigoPonto)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(codigoPonto);
                if (ponto == null)
                {
                    return null;
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    return DatabaseAcess.BuscarAlarmesPorPonto(codigoPonto);
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