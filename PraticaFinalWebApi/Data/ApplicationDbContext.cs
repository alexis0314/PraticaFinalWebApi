using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Models;
using static System.Collections.Specialized.BitVector32;

namespace PracticaFinalWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Profesor> Profesores { get; set; }

        public DbSet<Materia> Materias { get; set; }

        public DbSet<ProgramaAcademico> ProgramasAcademicos { get; set; }

        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }

        public DbSet<Seccion> Secciones { get; set; }

        public DbSet<TipoEvaluacion> TiposEvaluacion { get; set; }

        public DbSet<EstadoAcademico> EstadosAcademicos { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }
    }
}