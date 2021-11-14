using Dal;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public static class PublicoBll
    {
        private readonly static IDaoProducto daoProducto = FabricaDaos.ObtenerDaoProducto(Tipos.Entity);
        private readonly static IDaoUsuario daoUsuario = FabricaDaos.ObtenerDaoUsuario(Tipos.Entity);

        public static IEnumerable<Producto> ObtenerProductos()
        {
            return daoProducto.ObtenerTodos();
        }

        public static Producto BuscarProductoPorId(long id)
        {
            return daoProducto.ObtenerPorId(id);
        }

        public static IEnumerable<Producto> BuscarProductoPorNombre(string nombre)
        {
            return daoProducto.BuscarPorNombre(nombre);
        }
        public static decimal PrecioConDescuento(Producto producto)
        {
            return producto.Precio - (producto.Precio * (producto.Descuento / 100));
        }
        public static decimal PrecioConDescuento(decimal precio, decimal descuento)
        {
            return precio - (precio * (descuento / 100));
        }

        public static void AltaUsuario(Usuario usuario)
        {
            daoUsuario.Insertar(usuario);
        }
    }
}
