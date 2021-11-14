using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Carrito;

namespace Dal
{
    public interface IDaoCarrito : IDao<Carrito>
    {
        Detalle ObtenerDetalle(long carritoId, long productoId);
        void InsertarDetalle(Detalle detalle);
        void ModificarDetalle(Detalle detalle);
        void BorrarDetalle(long carritoId, long productoId);
    }
}
