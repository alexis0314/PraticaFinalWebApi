using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class PeriodoAcademico
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}