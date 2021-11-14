using Dal;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public static class ProductosBll
    {
        private static readonly IDaoProducto dao = FabricaDaos.ObtenerDaoProducto(Tipos.Entity);
        public static IEnumerable<Producto> Consultar(Usuario usuario)
        {
            Validar(usuario);
            return dao.ObtenerTodos();
        }


        public static Producto BuscarPorId(Usuario usuario, long? id)
        {
            Validar(usuario);
            return dao.ObtenerPorId(id.Value);
        }

        public static void Guardar(Usuario usuario, Producto producto)
        {
            Validar(usuario);
            dao.Insertar(producto);
        }

        public static void Modificar(Usuario usuario, Producto producto)
        {
            Validar(usuario);
            dao.Modificar(producto);
        }

        public static void Borrar(Usuario usuario, long id)
        {
            Validar(usuario);
            dao.Borrar(id);
        }
        private static void Validar(Usuario usuario)
        {
            if (usuario == null || usuario.Rol != "ADMIN")
            {
                throw new UnauthorizedAccessException("Sólo se admiten usuarios administradores: " + usuario);
            }
        }

    }
}
