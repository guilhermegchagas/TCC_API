using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class Alarme
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoCondicao { get; set; }
        public int TipoMedicao { get; set; }
        public DateTime HorarioAtualizacao { get; set; }
        public int CodigoPonto { get; set; }
    }
}