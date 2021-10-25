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
        public double PotenciaTotal { get; set; }
        public double PotenciaReativa { get; set; }
        public double FatorPotencia { get; set; }
        public double Corrente { get; set; }
        public double Tensao { get; set; }
        public double Frequencia { get; set; }
        public int CodigoPonto { get; set; }
    }
}