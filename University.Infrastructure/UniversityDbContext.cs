using Microsoft.EntityFrameworkCore;
using University.Domain.Entity.Course;
using University.Domain.Entity.File;
using University.Domain.Entity.Group;
using University.Domain.Entity.Lesson;
using University.Domain.Entity.Task;
using University.Domain.Entity.TaskAnswer;
using University.Domain.Entity.User;

namespace University.Infrastructure
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<GroupEntity> Groups { get; set; } = null!;
        public DbSet<LessonEntity> Lessons { get; set; } = null!;
        public DbSet<CourseEntity> Courses { get; set; } = null!;
        public DbSet<TaskEntity> Tasks { get; set; } = null!;
        public DbSet<TaskAnswerEntity> TaskAnswers { get; set; } = null!;
        public DbSet<FileEntity> Files { get; set; } = null!;

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.TaxId)
                .IsUnique();
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<GroupEntity>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<TaskAnswerEntity>()
                .HasIndex(ta => new { ta.TaskId, ta.StudentId })
                .IsUnique();

            modelBuilder.Entity<CourseEntity>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Students)
                .UsingEntity(j => j.ToTable("UserGroup"));

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.TaskAnswers)
                .WithOne(ta => ta.Student)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.CheckedTaskAnswers)
                .WithOne(ta => ta.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Lessons)
                .WithOne(l => l.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Files)
                .WithOne(f => f.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.TeachGroups)
                .WithOne(g => g.Teacher)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupEntity>()
                .HasMany(g => g.Lessons)
                .WithOne(l => l.Group)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupEntity>()
                .HasMany(g => g.Tasks)
                .WithOne(t => t.Group)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEntity>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEntity>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Course)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEntity>()
                .HasMany(c => c.Groups)
                .WithMany(g => g.Courses)
                .UsingEntity(j => j.ToTable("GroupCourse"));

            modelBuilder.Entity<TaskAnswerEntity>()
                .HasMany(ta => ta.Files)
                .WithOne(f => f.TaskAnswer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskEntity>()
                .HasMany(t => t.TaskAnswers)
                .WithOne(ta => ta.Task)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
