using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaFinalWebApi.Models
{
    public class Estudiante
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Matricula { get; set; } = string.Empty;

        [Required]
        public int ProgramaAcademicoId { get; set; }

        [Required]
        public int EstadoAcademicoId { get; set; }

        [Required]
        public int SeccionId { get; set; }

        [ForeignKey(nameof(ProgramaAcademicoId))]
        public ProgramaAcademico? ProgramaAcademico { get; set; }

        [ForeignKey(nameof(EstadoAcademicoId))]
        public EstadoAcademico? EstadoAcademico { get; set; }

        [ForeignKey(nameof(SeccionId))]
        public Seccion? Seccion { get; set; }

        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}