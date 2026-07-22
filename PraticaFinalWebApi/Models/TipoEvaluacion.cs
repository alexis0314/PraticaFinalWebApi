using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.Models
{
    public class TipoEvaluacion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}