using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class Seccion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}