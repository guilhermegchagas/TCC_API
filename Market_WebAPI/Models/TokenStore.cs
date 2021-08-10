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
            var dataFile = "c:\\temp\\" + id + ".txt";
            //var dataFile = "h:\\root\\home\\reciclado-001\\www\\reciclado\\" + cpf + ".txt";
            var data = File.ReadAllText(@dataFile);
            return JsonConvert.DeserializeObject<TokenStore>(data);
        }
    }
}