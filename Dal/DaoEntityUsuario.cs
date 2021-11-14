using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DaoEntityUsuario : IDaoUsuario
    {
        public void Borrar(long id)
        {
            using (var db = new TiendaContext())
            {
                db.Usuarios.Remove(db.Usuarios.Find(id));
                db.SaveChanges();
            }
        }

        public Usuario Insertar(Usuario usuario)
        {
            using (var db = new TiendaContext())
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return usuario;
            }
        }

        public Usuario Modificar(Usuario usuario)
        {
            using (var db = new TiendaContext())
            {
                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return usuario;
            }
        }

        public Usuario ObtenerPorId(long id)
        {
            using (var db = new TiendaContext())
            {
                return db.Usuarios.Find(id);
            }
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            using (var db = new TiendaContext())
            {
                return db.Usuarios.ToList();
            }
        }
        public Usuario ObtenerPorEmail(string email)
        {
            using (var db = new TiendaContext())
            {
                return db.Usuarios.Where(u => u.Email == email).FirstOrDefault();
            }
        }
    }
}
