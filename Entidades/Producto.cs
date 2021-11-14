using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Table("Productos")]
    public class Producto
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression(@"(\d+,?\d*)", ErrorMessage = "El número debe ser sólo dígitos con o sin decimales con coma")]
        [Range(0.0, 1000000.0)]
        public decimal Precio { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [RegularExpression(@"^[a-z0-9_]+(\.jpg|\.png)$", ErrorMessage = "Sólo ficheros jpg o png y con letras, números o guiones bajos")]
        public string Foto { get; set; }
        [Required]
        public string Unidad { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression(@"(\d+,?\d*)", ErrorMessage = "El número debe ser sólo dígitos con o sin decimales con coma")]
        [Range(0.0, 1000000.0)]
        public decimal PrecioUnidad { get; set; }
        [Range(0.0, 100.0)]
        public decimal Descuento { get; set; }
        public decimal PrecioConDescuento => Precio - (Precio * (Descuento / 100));
    }
}
