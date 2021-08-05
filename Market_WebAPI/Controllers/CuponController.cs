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
    public class CuponController : ApiController
    {
        /*
        public List<Cupom> GetAvailableCupons()
        {
            try
            {
                List<Cupom> cuponsDisponiveis = DatabaseAcess.BuscarCuponsDisponiveis();
                return cuponsDisponiveis;
            }
            catch
            {
                return null;
            }
        }

        public List<CupomResgatado> GetMyCupons(string cpf)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(cpf);
                if (tokenStore.token == token)
                {
                    List<CupomResgatado> cuponsDisponiveis = DatabaseAcess.BuscarMeusCupons(cpf);
                    return cuponsDisponiveis;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string PostResgatarCupom(string cpf, string codigo_cupom)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(cpf);
                if (tokenStore.token == token)
                {
                    if(DatabaseAcess.ResgatarCupom(cpf,codigo_cupom))
                    {
                        return "Cupom resgatado com sucesso!";
                    }       
                }
                return "Falha ao resgatar cupom.";
            }
            catch
            {
                return "Falha ao resgatar cupom.";
            }
        }
        */
    }
}