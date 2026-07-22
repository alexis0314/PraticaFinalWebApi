using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class EstadoAcademico
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}