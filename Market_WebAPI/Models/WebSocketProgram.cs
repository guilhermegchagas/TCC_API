using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace Market_WebAPI.Models
{
    public class WebSocketProgram
    {
        public static void Main()
        {
            var wssv = new WebSocketServer("ws://192.168.15.10:4649");
            //var wssv = new HttpServer("http://reciclado-001-site1.etempurl.com:1000");
            wssv.AddWebSocketService<ReciclagemSocket>("/ReciclagemSocket");
            wssv.Start();
        }
    }
}