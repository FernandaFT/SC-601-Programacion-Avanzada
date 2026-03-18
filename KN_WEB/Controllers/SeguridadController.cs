using KN_WEB.EntityFramework;
using KN_WEB.Filters;
using KN_WEB.Models;
using KN_WEB.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [SesionActiva]
    public class SeguridadController : Controller
    {
        readonly Generales generales = new Generales();

        #region Cambiar Accesos
        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarAcceso(SeguridadModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var context = new KN_DBEntities())
            {
                var consecutivoSesion = int.Parse(Session["Consecutivo"].ToString());
                var actualizacion = context.ActualizarContrasenna(model.ContrasennaNueva, consecutivoSesion);

                if (actualizacion <= 0)
                {
                    ViewBag.Mensaje = "Su información no se actualizó correctamente.";
                    return View();
                }

                //Se envía un correo electrónico al usuario con la nueva contraseña
                string rutaHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "NotificarContrasenna.html");
                string contenidoHtml = System.IO.File.ReadAllText(rutaHtml);

                string htmlFinal = contenidoHtml
                    .Replace("{{NOMBRE_USUARIO}}", Session["Nombre"].ToString());

                generales.EnviarCorreo(Session["CorreoElectronico"].ToString(), "Notificación de Acceso", htmlFinal);

                return RedirectToAction("CerrarSesion", "Home");
            }
        }

        #endregion

        #region Cambiar Perfil
        [HttpGet]
        public ActionResult CambiarPerfil()
        {
            using (var context = new KN_DBEntities())
            {
                var consecutivo = int.Parse(Session["Consecutivo"].ToString());
                var result = context.tUsuario.Where(p => p.Consecutivo == consecutivo).FirstOrDefault();

                var dto = new PerfilModel
                {
                    Identificacion = result.Identificacion,
                    Nombre = result.Nombre,
                    CorreoElectronico = result.CorreoElectronico
                };

                return View(dto);
            }
        }

        [HttpPost]
        public ActionResult CambiarPerfil(PerfilModel model, HttpPostedFileBase ImagenUsuario)
        {
            using (var context = new KN_DBEntities())
            {
                var consecutivo = int.Parse(Session["Consecutivo"].ToString());
                var result = context.tUsuario.Where(p => p.Consecutivo == consecutivo).FirstOrDefault();

                if (result != null)
                {
                    result.Identificacion = model.Identificacion;
                    result.Nombre = model.Nombre;
                    result.CorreoElectronico = model.CorreoElectronico;

                    if (ImagenUsuario != null && ImagenUsuario.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(ImagenUsuario.FileName);
                        string fileName = consecutivo + extension;
                        string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
                        string path = Path.Combine(folder, fileName);

                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);

                        ImagenUsuario.SaveAs(path);
                        result.ImagenUsuario = "/Uploads/" + fileName;
                    }

                    context.SaveChanges();
                }

                Session["Nombre"] = model.Nombre;
                Session["CorreoElectronico"] = model.CorreoElectronico;

                if (ImagenUsuario != null && ImagenUsuario.ContentLength > 0)
                    Session["ImagenUsuario"] = result.ImagenUsuario;

                return RedirectToAction("Index", "Home");
            }

        }

        #endregion
    }
}