using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaFinalWebApi.Models
{
    public class Calificacion
    {
        public int Id { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        public int PeriodoAcademicoId { get; set; }

        [Required]
        public int TipoEvaluacionId { get; set; }

        [Range(0, 100)]
        public decimal Calificacion1 { get; set; }

        [Range(0, 100)]
        public decimal Calificacion2 { get; set; }

        [Range(0, 100)]
        public decimal Calificacion3 { get; set; }

        [Range(0, 100)]
        public decimal Calificacion4 { get; set; }

        [Range(0, 100)]
        public decimal Examen { get; set; }

        public decimal TotalCalificacion { get; set; }

        [StringLength(1)]
        public string Clasificacion { get; set; } = string.Empty;

        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;

        [ForeignKey(nameof(EstudianteId))]
        public Estudiante? Estudiante { get; set; }

        [ForeignKey(nameof(MateriaId))]
        public Materia? Materia { get; set; }

        [ForeignKey(nameof(PeriodoAcademicoId))]
        public PeriodoAcademico? PeriodoAcademico { get; set; }

        [ForeignKey(nameof(TipoEvaluacionId))]
        public TipoEvaluacion? TipoEvaluacion { get; set; }
    }
}