using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Table("Usuarios")]
    public class Usuario
    {
        public long? Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*[%\\-]).{8,64})$", ErrorMessage = "Debe tener al menos un dígito, una letra minúscula y una letra mayúscula")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [RegularExpression(@"^[\p{L} ]*$")]
        public string Nombre { get; set; }
        [Required]
        public string Rol { get; set; } = "USER";

        public Carrito Carrito { get; set; }
    }
}
