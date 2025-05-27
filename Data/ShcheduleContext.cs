namespace ScheduleServer.Data;
using Microsoft.EntityFrameworkCore;
using ScheduleServer.Models;

    public class ShcheduleContext : DbContext
    {
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Assignments> assignments { get; set; }

        public ShcheduleContext(DbContextOptions<ShcheduleContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(t => t.name).IsRequired().HasMaxLength(50);
                entity.HasOne(t => t.department)
                    .WithMany(d => d.teachers)
                    .HasForeignKey(t => t.department_id)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(t => t.department_id);
            });


            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasOne(g => g.department)
                    .WithMany(d => d.groups)
                    .HasForeignKey(g => g.department_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");
                entity.HasOne(s => s.teacher).WithMany().HasForeignKey(s => s.teacher_id);
                entity.HasOne(s => s.subject).WithMany().HasForeignKey(s => s.subject_id);
                entity.HasOne(s => s.group).WithMany().HasForeignKey(s => s.group_id);
                entity.HasIndex(s => new { s.day_of_week, s.week_type });
                entity.HasIndex(s => s.room);
                entity.Property(p => p.start_time).HasColumnName("start_time");
                entity.Property(p => p.end_time).HasColumnName("end_time");
            });


            modelBuilder.Entity<Assignments>(entity =>
            {
                entity.HasOne(a => a.teacher)
                    .WithMany(t => t.assignments)
                    .HasForeignKey(a => a.teacher_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.subject)
                    .WithMany(s => s.assignment)
                    .HasForeignKey(a => a.subject_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.group)
                    .WithMany(g => g.assignments)
                    .HasForeignKey(a => a.group_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable(t => t.HasCheckConstraint("CK_Assignment_Dates", "start_date <= end_date"));
                entity.HasIndex(a => a.teacher_id);
                entity.HasIndex(a => a.group_id);
                entity.HasIndex(a => new { a.start_date, a.end_date });
                entity.Property(a => a.start_date).HasColumnType("date");
                entity.Property(a => a.end_date).HasColumnType("date");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subjects");
                entity.Property(s => s.department_id).HasColumnName("department_id");
                entity.Property(s => s.name).IsRequired().HasMaxLength(100);

                entity.HasOne(s => s.department)
                    .WithMany(d => d.subjects)
                    .HasForeignKey(s => s.department_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(s => s.assignment)
                    .WithOne(a => a.subject)
                    .HasForeignKey(a => a.subject_id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(s => s.department_id);
                entity.HasIndex(s => s.name);
            });


            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");
                entity.HasIndex(d => d.name).IsUnique();
            });
        }
    }
