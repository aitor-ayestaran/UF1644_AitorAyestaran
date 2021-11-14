using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Table("Carritos")]
    public class Carrito
    {
        public long? Id { get; set; }
        [Required]
        public Usuario Usuario { get; set; }

        public ICollection<Detalle> Detalles { get; set; }

        public class Detalle
        {
            [Key, Column(Order = 0)]
            public long? CarritoId { get; set; }
            [Key, Column(Order = 1)]
            public long? ProductoId { get; set; }
            [ForeignKey("CarritoId")]
            public Carrito Carrito { get; set; }
            [ForeignKey("ProductoId")]
            public Producto Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio => Producto.PrecioConDescuento * Cantidad;
        }

        public decimal Precio => Detalles.Sum(d => d.Precio);
    }
}
