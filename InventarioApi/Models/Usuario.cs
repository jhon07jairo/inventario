using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Nombre { get; set; }

        [Required, StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public string Rol { get; set; } = "Usuario";
    }
}
