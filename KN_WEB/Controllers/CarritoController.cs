using KN_WEB.EntityFramework;
using KN_WEB.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [SesionActiva]
    public class CarritoController : Controller
    {

        [HttpPost]
        public ActionResult GuardarServicioCarrito(int servicioId, int horarioId)
        {
            using (var context = new KN_DBEntities())
            {
                var tabla = new tCarrito
                {
                    ConsecutivoUsuario = int.Parse(Session["Consecutivo"].ToString()),
                    ConsecutivoServicio = servicioId,
                    FechaHoraCarrito = DateTime.Now,
                    ConsecutivoHorario = horarioId,
                    Estado = 1 // Pendiente
                };

                context.tCarrito.Add(tabla);
                context.SaveChanges();
                return Json("Servicio Agregado al Carrito", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ConsultarCarrito()
        {
            return View();
        }
    }
}