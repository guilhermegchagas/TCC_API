using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class Medicao
    {
        public int Codigo { get; set; }
        public DateTime Horario { get; set; }
        public double Potencia { get; set; }
        public int CodigoPonto { get; set; }
    }
}