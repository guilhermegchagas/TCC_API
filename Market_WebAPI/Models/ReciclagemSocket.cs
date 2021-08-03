using System;
using System.Collections.Generic;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Market_WebAPI.Models
{
    public class ReciclagemSocket : WebSocketBehavior
    {
        private static List<ReciclagemSocket> SocketsAtivos = new List<ReciclagemSocket>();
        public string CPF;
        public string ShortToken;
        public CarrinhoReciclagem Carrinho;

        protected override void OnMessage(MessageEventArgs e)
        {
            if(e.Data == "Finalizar")
            {
                try
                {
                    if(FinalizarColeta())
                        Send("FinalizarOK");
                    else
                        Send("FinalizarFailed");
                }
                catch
                {
                    Send("FinalizarFailed");
                }
            } 
        }

        protected override void OnOpen()
        {
            CPF = Context.QueryString["CPF"];
            ShortToken = Context.QueryString["ShortToken"];
            ReciclagemSocket socketCPFIgual = FindSocketByCPF(CPF);
            ReciclagemSocket socketTokenIgual = FindSocketByCPF(ShortToken);
            SocketsAtivos.Remove(socketCPFIgual);
            SocketsAtivos.Remove(socketTokenIgual);
            SocketsAtivos.Add(this);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            FinalizarColeta();
            SocketsAtivos.Remove(this);
        }

        public void SendData(string data)
        {
            Send(data);
        }

        public bool FinalizarColeta()
        {
            if (Carrinho.listaItems.Count != 0)
            {
                if (DatabaseAcess.CadastrarReciclagem(CPF, Carrinho.listaItems))
                    return true;
                else
                    return false;
            }
            else
                return false;
            
        }

        public static ReciclagemSocket FindSocketByToken(string ShortToken)
        {
            return SocketsAtivos.Find(socket => socket.ShortToken == ShortToken);
        }

        public static ReciclagemSocket FindSocketByCPF(string CPF)
        {
            return SocketsAtivos.Find(socket => socket.CPF == CPF);
        }
    }
}