using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class CupomResgatado
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Vencimento { get; set; }
        public string CorHex { get; set; }
    }
}