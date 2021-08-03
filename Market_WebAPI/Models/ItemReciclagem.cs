using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class ItemReciclagem
    {
        public string codigo_qr { get; set; }
        public string codigo_barra_produto { get; set; }
        public string nome_produto { get; set; }
        public double vl_item_reciclagem { get; set; }
    }
}