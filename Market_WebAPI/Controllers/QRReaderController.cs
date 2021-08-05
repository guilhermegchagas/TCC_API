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
    public class QRReaderController : ApiController
    {
        /*
        public string PostIniciarColeta()
        {
            try
            {
                string ShortToken = ActionContext.Request.Headers.Authorization.Parameter;
                ReciclagemSocket socket = ReciclagemSocket.FindSocketByToken(ShortToken);
                if(socket.ShortToken == ShortToken)
                {
                    CarrinhoReciclagem carrinho = new CarrinhoReciclagem();
                    socket.Carrinho = carrinho;
                    socket.SendData("Start");
                    return "OK";
                }
                return "Failed";
            }
            catch
            {
                return "Failed";
            }
        }

        public string PostItemReciclagem(string qr)
        {
            try
            {
                string ShortToken = ActionContext.Request.Headers.Authorization.Parameter;
                ReciclagemSocket socket = ReciclagemSocket.FindSocketByToken(ShortToken);
                if (socket.ShortToken == ShortToken)
                {
                    ItemReciclagem item = DatabaseAcess.CarregarItemReciclagem(qr);
                    if(item == null)
                        return "Failed";
                    if(socket.Carrinho.listaItems.Find(i => i.codigo_qr == qr) == null)
                    {
                        socket.Carrinho.AddItem(item);
                        string data = JsonConvert.SerializeObject(item);
                        socket.SendData(data);
                        return "OK";
                    }
                    else
                        return "Failed";
                }
                return "Failed";
            }
            catch
            {
                return "Failed";
            }
        }
        */
    }
}