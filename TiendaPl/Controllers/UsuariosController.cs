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
    public class UsuariosController : Controller
    {
        //private TiendaContext db = new TiendaContext();
        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = Bll.UsuariosBll.Consultar(Session["usuario"] as Usuario);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = Bll.UsuariosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(db.Carritos, "Id", "Id");
            return View(new Usuario());
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,Nombre,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = Bll.UsuariosBll.ObtenerHash(usuario.Password);
                Bll.UsuariosBll.Guardar(Session["usuario"] as Usuario, usuario);
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.Carritos, "Id", "Id", usuario.Id);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = Bll.UsuariosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(db.Carritos, "Id", "Id", usuario.Id);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Nombre,Password,Rol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Bll.UsuariosBll.Modificar(Session["usuario"] as Usuario, usuario);
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.Carritos, "Id", "Id", usuario.Id);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = Bll.UsuariosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Bll.UsuariosBll.Borrar(Session["usuario"] as Usuario, id);
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Admin/5
        public ActionResult Admin(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = Bll.UsuariosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Admin/5
        [HttpPost, ActionName("Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AdminConfirmed(long id)
        {
            Bll.UsuariosBll.DarPermisos(Session["usuario"] as Usuario, id);
            return RedirectToAction("Index");
        }

        // GET: Usuarios/User/5
        public ActionResult User(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = Bll.UsuariosBll.BuscarPorId(Session["usuario"] as Usuario, id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/User/5
        [HttpPost, ActionName("User")]
        [ValidateAntiForgeryToken]
        public ActionResult UserConfirmed(long id)
        {
            Bll.UsuariosBll.QuitarPermisos(Session["usuario"] as Usuario, id);
            return RedirectToAction("Index");
        }
    }
}
