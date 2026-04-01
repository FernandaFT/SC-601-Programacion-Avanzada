using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_WEB.Models
{
    public class HorarioModel
    {
        public int Consecutivo { get; set; }
        public int ConsecutivoServicio { get; set; }
        public DateTime FechaDisponible { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}