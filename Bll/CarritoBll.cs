using Dal;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Carrito;

namespace Bll
{
    public static class CarritoBll
    {
        private readonly static IDaoCarrito dao = FabricaDaos.ObtenerDaoCarrito(Tipos.Entity);

        public static Carrito Obtener(Usuario usuario)
        {
            Validar(usuario);
            Carrito carrito = dao.ObtenerPorId(usuario.Id.Value);

            if (carrito == null)
            {
                carrito = Alta(usuario);
            }

            return carrito;
        }
        private static Carrito Alta(Usuario usuario)
        {
            Validar(usuario);
            return dao.Insertar(new Carrito() { Usuario = usuario });
        }

        public static void Baja(Usuario usuario)
        {
            Validar(usuario);
            dao.Borrar(usuario.Id.Value);
        }

        public static void AgregarProducto(long usuarioId, long productoId, int cantidad = 1)
        {
            Detalle detalle = dao.ObtenerDetalle(usuarioId, productoId);

            if (detalle != null)
            {
                detalle.Cantidad += cantidad;
                ModificarProducto(detalle);
            }
            else
            {
                InsertarProducto(usuarioId, productoId, cantidad);
            }
        }
        public static void RestarProducto(long usuarioId, long productoId)
        {
            Detalle detalle = dao.ObtenerDetalle(usuarioId, productoId);

            if (detalle != null)
            {
                if (detalle.Cantidad > 1)
                {
                    detalle.Cantidad -= 1;
                    ModificarProducto(detalle);
                }
            }
        }
        private static void InsertarProducto(long usuarioId, long productoId, int cantidad = 1)
        {
            Detalle detalle = new Detalle() { CarritoId = usuarioId, ProductoId = productoId, Cantidad = cantidad };
            dao.InsertarDetalle(detalle);
        }

        public static void ModificarProducto(Detalle detalle)
        {
            dao.ModificarDetalle(detalle);
        }

        public static void ModificarProducto(long usuarioId, long productoId, int cantidad = 1)
        {
            Detalle detalle = new Detalle() { CarritoId = usuarioId, ProductoId = productoId, Cantidad = cantidad };
            dao.ModificarDetalle(detalle);
        }

        public static void BorrarDetalle(long usuarioId, long productoId)
        {
            dao.BorrarDetalle(usuarioId, productoId);
        }

        private static void Validar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Debes ser un usuario registrado para poder usar el carrito");
            }
        }
    }
}
