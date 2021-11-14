using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DaoEntityCarrito : IDaoCarrito
    {
        public void Borrar(long id)
        {
            using (var db = new TiendaContext())
            {
                db.Carritos.Remove(db.Carritos.Find(id));
                db.SaveChanges();
            }
        }

        public void BorrarDetalle(long carritoId, long productoId)
        {
            using (var db = new TiendaContext())
            {
                db.Detalles.Remove(db.Detalles.Find(carritoId, productoId));
                db.SaveChanges();
            }
        }

        public Carrito Insertar(Carrito carrito)
        {
            using (var db = new TiendaContext())
            {
                db.Entry(carrito.Usuario).State = System.Data.Entity.EntityState.Unchanged;
                db.Carritos.Add(carrito);
                db.SaveChanges();
                return carrito;
            }
        }

        public void InsertarDetalle(Carrito.Detalle detalle)
        {
            using (var db = new TiendaContext())
            {
                db.Detalles.Add(detalle);
                db.SaveChanges();
            }

        }

        public Carrito Modificar(Carrito carrito)
        {
            throw new NotSupportedException("No es una operación válida para carrito");
        }

        public void ModificarDetalle(Carrito.Detalle detalle)
        {
            using (var db = new TiendaContext())
            {
                db.Entry(detalle).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Carrito.Detalle ObtenerDetalle(long carritoId, long productoId)
        {
            using (var db = new TiendaContext())
            {
                return db.Detalles.Find(carritoId, productoId);
            }
        }

        public Carrito ObtenerPorId(long id)
        {
            using (var db = new TiendaContext())
            {
                return db.Carritos.Include("Usuario").Include("Detalles").Include("Detalles.Producto").FirstOrDefault(c => c.Id == id);

            }
        }

        public IEnumerable<Carrito> ObtenerTodos()
        {
            using (var db = new TiendaContext())
            {
                return db.Carritos.ToList();

            }
        }
    }
}
