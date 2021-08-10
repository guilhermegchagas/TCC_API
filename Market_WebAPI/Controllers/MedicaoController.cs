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
                int codigoUsuario = DatabaseAcess.BuscarPontoPorCodigo(medicao.CodigoPonto).CodigoUsuario;
                TokenStore tokenStore = TokenStore.GetTokenStore(codigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.CadastrarMedicao(medicao);
                    return Request.CreateResponse(HttpStatusCode.Created, "Medição cadastrada.");
                }
                return null;
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
                int codigoUsuario = DatabaseAcess.BuscarPontoPorCodigo(codigoPonto).CodigoUsuario;
                TokenStore tokenStore = TokenStore.GetTokenStore(codigoUsuario);
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