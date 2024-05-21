using Microsoft.EntityFrameworkCore;
using University.API.Models;

namespace University.API
{
    public class UniversityContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<GroupModel> Groups { get; set; } = null!;
        public DbSet<LessonModel> Lessons { get; set; } = null!;
        public DbSet<CourseModel> Courses { get; set; } = null!;
        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<TaskAnswerModel> TaskAnswers { get; set; } = null!;
        public DbSet<FileModel> Files { get; set; } = null!;

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.TaxId)
                .IsUnique();
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<GroupModel>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<TaskAnswerModel>()
                .HasIndex(ta => new { ta.TaskId, ta.StudentId })
                .IsUnique();

            modelBuilder.Entity<CourseModel>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
