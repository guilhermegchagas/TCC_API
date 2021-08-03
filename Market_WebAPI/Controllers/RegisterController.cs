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
    public class RegisterController : ApiController
    {
        // POST api/register
        public HttpResponseMessage PostCliente([FromBody]Cliente cliente)
        {
            HttpResponseMessage response = null;
            try
            {
                if(!DatabaseAcess.ValidarCPFCliente(cliente.CPF))
                {
                    response = Request.CreateResponse(HttpStatusCode.Created, "CPF já existe.");
                    return response;
                }
                if (!DatabaseAcess.ValidarEmailCliente(cliente.Email))
                {
                    response = Request.CreateResponse(HttpStatusCode.Created, "Email já existe.");
                    return response;
                }
                DatabaseAcess.CadastrarCliente(cliente);
                response = Request.CreateResponse(HttpStatusCode.Created, "Cliente cadastrado.");
                return response;
            }
            catch
            {
                response = Request.CreateResponse(HttpStatusCode.Created,"Falha ao conectar com o banco.");
                return response;
            }
        }
    }
}
