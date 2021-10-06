using Market_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Market_WebAPI.Controllers
{
    public class MedicaoController : ApiController
    {
        public class DecodedPayload
        {
            public int codigoPonto { get; set; }
            public string senhaUsuario { get; set; }
            public double potenciaTotal { get; set; }
            public double potenciaAtiva { get; set; }
            public double potenciaReativa { get; set; }
            public double fatorPotencia { get; set; }
            public double corrente { get; set; }
            public double tensao { get; set; }
            public double frequencia { get; set; }
        }

        public class UplinkMessage
        {
            public DecodedPayload decoded_payload { get; set; }
        }

        public class DadosMedicao
        {
            public DateTime received_at { get; set; }
            public UplinkMessage uplink_message { get; set; }
        }

        [HttpPost]
        [Route("api/medicao/cadastrar")]
        public HttpResponseMessage PostMedicao([FromBody] DadosMedicao dadosMedicao)
        {
            try
            {
                Medicao medicao = new Medicao();
                medicao.Horario = dadosMedicao.received_at;
                medicao.PotenciaTotal = dadosMedicao.uplink_message.decoded_payload.potenciaTotal;
                medicao.PotenciaAtiva = dadosMedicao.uplink_message.decoded_payload.potenciaAtiva;
                medicao.PotenciaReativa = dadosMedicao.uplink_message.decoded_payload.potenciaReativa;
                medicao.FatorPotencia = dadosMedicao.uplink_message.decoded_payload.fatorPotencia;
                medicao.Corrente = dadosMedicao.uplink_message.decoded_payload.corrente;
                medicao.Tensao = dadosMedicao.uplink_message.decoded_payload.tensao;
                medicao.Frequencia = dadosMedicao.uplink_message.decoded_payload.frequencia;
                medicao.CodigoPonto = dadosMedicao.uplink_message.decoded_payload.codigoPonto;

                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(medicao.CodigoPonto);
                if (ponto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ponto de medição não existe.");
                }
                Usuario usuario = DatabaseAcess.BuscarUsuarioPorID(ponto.CodigoUsuario);
                if (usuario == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "O usuário deste ponto não existe.");
                }
                if (dadosMedicao.uplink_message.decoded_payload.senhaUsuario != usuario.Senha)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Senha do usuário inválida.");
                }

                DatabaseAcess.CadastrarMedicao(medicao);
                return Request.CreateResponse(HttpStatusCode.Created, "Medição cadastrada.");           
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }

        public List<Medicao> GetMedicoes(int cp,
            DateTime? hi = null, DateTime? hf = null,
            double? pti = null, double? ptf = null,
            double? pai = null, double? paf = null,
            double? pri = null, double? prf = null,
            double? fpi = null, double? fpf = null,
            double? ci = null, double? cf = null,
            double? ti = null, double? tf = null,
            double? fi = null, double? ff = null)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                Ponto ponto = DatabaseAcess.BuscarPontoPorCodigo(cp);
                if(ponto == null)
                {
                    return null;
                }
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    return DatabaseAcess.BuscarMedicao(
                        cp,
                        hi, hf,
                        pti, ptf,
                        pai, paf,
                        pri, prf,
                        fpi, fpf,
                        ci, cf,
                        ti, tf,
                        fi, ff);
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