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

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Students)
                .UsingEntity(j => j.ToTable("UserGroup"));

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.TaskAnswers)
                .WithOne(ta => ta.Student)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Lessons)
                .WithOne(l => l.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Files)
                .WithOne(f => f.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.TeachGroups)
                .WithOne(g => g.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupModel>()
                .HasMany(g => g.Lessons)
                .WithOne(l => l.Group)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupModel>()
                .HasMany(g => g.Tasks)
                .WithOne(t => t.Group)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseModel>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseModel>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Course)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseModel>()
                .HasMany(c => c.Groups)
                .WithMany(g => g.Courses)
                .UsingEntity(j => j.ToTable("GroupCourse"));

            modelBuilder.Entity<TaskAnswerModel>()
                .HasMany(ta => ta.Files)
                .WithOne(f => f.TaskAnswer)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
