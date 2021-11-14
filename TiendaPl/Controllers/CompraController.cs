using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaPl.Controllers
{
    public class CompraController : Controller
    {
       

        public ActionResult AgregarCarrito(long id, int cantidad)
        {
            Carrito carrito = Bll.CarritoBll.Obtener((Entidades.Usuario)Session["usuario"]);


            Bll.CarritoBll.AgregarProducto(carrito.Id.Value, id, cantidad);

            return RedirectToAction("Carrito");
        }
        public ActionResult RestarCarrito(long id)
        {
            Carrito carrito = Bll.CarritoBll.Obtener((Entidades.Usuario)Session["usuario"]);

            Bll.CarritoBll.RestarProducto(carrito.Id.Value, id);

            return RedirectToAction("Carrito");
        }

        public ActionResult BorrarProducto(long id)
        {
            Carrito carrito = Bll.CarritoBll.Obtener((Entidades.Usuario)Session["usuario"]);

            Bll.CarritoBll.BorrarDetalle(carrito.Id.Value, id);

            return RedirectToAction("Carrito");
        }
        public ActionResult Carrito()
        {
            Carrito carrito = Bll.CarritoBll.Obtener((Entidades.Usuario)Session["usuario"]);

            return View(carrito);
        }
    }
}
