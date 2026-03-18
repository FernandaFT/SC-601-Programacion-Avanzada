using System.ComponentModel.DataAnnotations;

namespace KN_WEB.Models
{
    public class PerfilModel
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string ImagenUsuario { get; set; }
    }
}