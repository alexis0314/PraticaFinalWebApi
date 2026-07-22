using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class ProgramaAcademico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del programa es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        // Propiedad de navegación
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}