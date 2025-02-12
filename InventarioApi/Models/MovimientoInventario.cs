using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class MovimientoInventario
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public string? TipoMovimiento { get; set; } // "Entrada" o "Salida"

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
