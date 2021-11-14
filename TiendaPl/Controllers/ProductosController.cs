using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades;

namespace TiendaPl.Controllers
{
    public class ProductosController : Controller
    {
        //private TiendaContext db = new TiendaContext();

        // GET: Productos
        public ActionResult Index()
        {
            return View(Bll.ProductosBll.Consultar(Session["usuario"] as Usuario));
        }

        // GET: Productos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = Bll.ProductosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View(new Producto());
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Precio,Foto,Unidad,PrecioUnidad,Descuento")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                Bll.ProductosBll.Guardar(Session["usuario"] as Usuario, producto);
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = Bll.ProductosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio,Foto,Unidad,PrecioUnidad,Descuento")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                Bll.ProductosBll.Modificar(Session["usuario"] as Usuario, producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = Bll.ProductosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Bll.ProductosBll.Borrar(Session["usuario"] as Usuario, id);
            return RedirectToAction("Index");
        }       
    }
}
