﻿using Market_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Market_WebAPI.Controllers
{
    public class PontoController : ApiController
    {
        [Route("api/ponto/cadastrar")]
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
        [Route("api/ponto/atualizar")]
        public HttpResponseMessage PutPonto([FromBody]Ponto ponto)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.AtualizarPonto(ponto);
                    return Request.CreateResponse(HttpStatusCode.Created, "Ponto atualizado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/ponto/atualizarKWH")]
        public HttpResponseMessage PutAtualizarKWH([FromBody]Ponto ponto)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(ponto.CodigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.AtualizarPrecoKWH(ponto);
                    return Request.CreateResponse(HttpStatusCode.Created, "Ponto atualizado.");
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao conectar com o banco.");
            }
        }
        [Route("api/ponto/deletar")]
        public HttpResponseMessage DeletePonto(int codigoUsuario, int codigoPonto)
        {
            try
            {
                string token = ActionContext.Request.Headers.Authorization.Parameter;
                TokenStore tokenStore = TokenStore.GetTokenStore(codigoUsuario);
                if (tokenStore.token == token)
                {
                    DatabaseAcess.DeletarPonto(codigoPonto);
                    return Request.CreateResponse(HttpStatusCode.Created, "Ponto deletado.");
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