using Casetask.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Casetask.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    DbSet<Student> Students { get; set; }
    DbSet<Teacher> Teachers { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<Score> Scores { get; set; }
    DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(e => e.Id);
        modelBuilder.Entity<Teacher>().HasKey(e => e.Id);
        modelBuilder.Entity<Subject>().HasKey(e => e.Id);
        modelBuilder.Entity<Score>().HasKey(e => e.Id);
        modelBuilder.Entity<User>().HasKey(e => e.Id);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Results)
            .WithOne(score => score.Student)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}