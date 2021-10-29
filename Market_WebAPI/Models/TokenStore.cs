using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class TokenStore
    {
        public string token { get; set; }
        public string ID { get; set; }

        public static TokenStore GetTokenStore(int id)
        {
            #if DEBUG
                var dataFile = "c:\\temp\\" + id + ".txt";
            #else
                var dataFile = "c:\\Inetpub\\vhosts\\guilherme2109300258.bateaquihost.com.br\\httpdocs\\tokenStore\\" + id + ".txt";
            #endif
            var data = File.ReadAllText(@dataFile);
            return JsonConvert.DeserializeObject<TokenStore>(data);
        }
    }
}