using System;
using System.Collections.Generic;

namespace KN_WEB.Models
{
    public class ServicioModel
    {
        public int Consecutivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Estado { get; set; }
        public string Video { get; set; }
        public List<HorarioModel> Horarios { get; set; } = new List<HorarioModel>();
    }
}
