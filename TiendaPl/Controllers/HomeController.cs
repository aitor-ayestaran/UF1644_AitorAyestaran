using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaPl.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(Bll.PublicoBll.ObtenerProductos());
        }

        public ActionResult Login()
        {
            return View(new UsuarioLogin());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] UsuarioLogin usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario verificado = Bll.UsuariosBll.Verificar(usuario);

                if (verificado != null)
                {
                    Session["usuario"] = verificado;
                    return RedirectToAction("Index");
                }
            }

            return View(usuario);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult DetalleProducto(long id)
        {
            return View(Bll.PublicoBll.BuscarProductoPorId(id));
        }

        public ActionResult Registro()
        {
            return View(new Usuario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Include = "Email,Password,Nombre")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {                
                usuario.Password = Bll.UsuariosBll.ObtenerHash(usuario.Password);
                Bll.PublicoBll.AltaUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Buscar(string nombre)
        {
            var productos = Bll.PublicoBll.BuscarProductoPorNombre(nombre);
            ViewBag.Buscar = nombre;

            return View(productos);
        }
    }
}