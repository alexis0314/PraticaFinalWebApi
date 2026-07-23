using Microsoft.EntityFrameworkCore;
using PracticaFinalWebApi.Models;

namespace PracticaFinalWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<ProgramaAcademico> ProgramasAcademicos { get; set; }

        public DbSet<EstadoAcademico> EstadosAcademicos { get; set; }

        public DbSet<Seccion> Secciones { get; set; }

        public DbSet<Profesor> Profesores { get; set; }

        public DbSet<Materia> Materias { get; set; }

        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }

        public DbSet<TipoEvaluacion> TiposEvaluacion { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasIndex(e => e.Matricula)
                .IsUnique();

            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Profesor)
                .WithMany(p => p.Materias)
                .HasForeignKey(m => m.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.ProgramaAcademico)
                .WithMany(p => p.Estudiantes)
                .HasForeignKey(e => e.ProgramaAcademicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.EstadoAcademico)
                .WithMany(ea => ea.Estudiantes)
                .HasForeignKey(e => e.EstadoAcademicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Seccion)
                .WithMany(s => s.Estudiantes)
                .HasForeignKey(e => e.SeccionId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Estudiante)
                .WithMany(e => e.Calificaciones)
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Materia)
                .WithMany(m => m.Calificaciones)
                .HasForeignKey(c => c.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.PeriodoAcademico)
                .WithMany(p => p.Calificaciones)
                .HasForeignKey(c => c.PeriodoAcademicoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.TipoEvaluacion)
                .WithMany(t => t.Calificaciones)
                .HasForeignKey(c => c.TipoEvaluacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.TipoEvaluacion)
                .WithMany(t => t.Calificaciones)
                .HasForeignKey(c => c.TipoEvaluacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.Calificacion1)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.Calificacion2)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.Calificacion3)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.Calificacion4)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.Examen)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Calificacion>()
                .Property(c => c.TotalCalificacion)
                .HasPrecision(5, 2);
        }

    }
}