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
    public class NotificacaoController : ApiController
    {
        [Route("api/notificacao/cadastrar")]
        public HttpResponseMessage PostNotificacao([FromBody]Notificacao notificacao)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(notificacao.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.CadastrarNotificacao(notificacao);
                    return Request.CreateResponse(HttpStatusCode.Created, "Notificação cadastrada.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/notificacao/deletar")]
        public HttpResponseMessage DeleteNotificacao(int codigo, int codigoPonto)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(codigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.DeletarNotificacao(codigo);
                    return Request.CreateResponse(HttpStatusCode.Created, "Notificação deletada.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/notificacao/limpar")]
        public HttpResponseMessage DeleteLimparNotificacao(int codigoPonto, DateTime? horarioInicial = null, DateTime? horarioFinal = null)
        {
            try
            {
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(codigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.LimparNotificacao(horarioInicial, horarioFinal);
                    return Request.CreateResponse(HttpStatusCode.Created, "Notificações limpadas.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }

        public List<Notificacao> GetNotificacoesPorPonto(int codigoPonto, DateTime? horarioInicial = null, DateTime? horarioFinal = null)
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
                    return DatabaseAcess.BuscarNotificacoesPorPonto(codigoPonto, horarioInicial, horarioFinal);
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