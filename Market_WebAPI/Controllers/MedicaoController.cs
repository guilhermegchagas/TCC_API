using Market_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace Market_WebAPI.Controllers
{
    public class MedicaoController : ApiController
    {
        public HttpResponseMessage PostMedicao([FromBody]Medicao medicao)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(medicao.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.CadastrarMedicao(medicao);
                    return Request.CreateResponse(HttpStatusCode.Created, "Medição cadastrada.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }

        public List<Medicao> GetMedicoes(int codigoPonto,
            DateTime? horarioInicial = null,
            DateTime? horarioFinal = null,
            double? potenciaInicial = null,
            double? potenciaFinal = null)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(codigoPonto);
                if(ponto == null)
                {
                    return null;
                }
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    return DatabaseAcess.BuscarMedicao(codigoPonto, horarioInicial, horarioFinal, potenciaInicial, potenciaFinal);
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