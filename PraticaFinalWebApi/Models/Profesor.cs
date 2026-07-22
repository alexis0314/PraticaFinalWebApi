using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class Profesor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }
}