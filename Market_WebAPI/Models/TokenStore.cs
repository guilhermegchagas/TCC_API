﻿using Newtonsoft.Json;
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
        public string CPF { get; set; }

        public static string GetShortToken(string Token)
        {
            return Token.Substring(0, 25);
        }

        public static TokenStore GetTokenStore(string cpf)
        {
            var dataFile = "c:\\temp\\" + cpf + ".txt";
            //var dataFile = "h:\\root\\home\\reciclado-001\\www\\reciclado\\" + cpf + ".txt";
            var data = File.ReadAllText(@dataFile);
            return JsonConvert.DeserializeObject<TokenStore>(data);
        }
    }
}