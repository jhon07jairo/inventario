using System;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Nombre { get; set; }

        [Required]
        public int Stock { get; set; } = 0;

        public DateTime FechaCreacion { get; set; } //= DateTime.UtcNow;
    }
}
