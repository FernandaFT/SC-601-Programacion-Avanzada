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
            var consecutivoUsuario = int.Parse(Session["Consecutivo"].ToString());

            using (var context = new KN_DBEntities())
            {
                var existeServicio = context.tCarrito.Where(p => p.ConsecutivoUsuario == consecutivoUsuario
                                                      && p.ConsecutivoServicio == servicioId).FirstOrDefault();

                if (existeServicio != null)
                    return Json(new { codigo = -1, mensaje = "El servicio ya existe en el carrito" },
                        JsonRequestBehavior.AllowGet);

                var tabla = new tCarrito
                {
                    ConsecutivoUsuario = consecutivoUsuario,
                    ConsecutivoServicio = servicioId,
                    FechaHoraCarrito = DateTime.Now,
                    ConsecutivoHorario = horarioId,
                    Estado = 1 // Pendiente
                };

                context.tCarrito.Add(tabla);
                context.SaveChanges();
                return Json(new { codigo = 0, mensaje = "El servicio se agregó correctamente al carrito" },
                        JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ConsultarCarrito()
        {
            var consecutivoUsuario = int.Parse(Session["Consecutivo"].ToString());

            using (var context = new KN_DBEntities())
            {
                var result = context.sp_ObtenerServiciosCarrito(consecutivoUsuario).ToList();
                return View(result);
            }
        }
    }
}