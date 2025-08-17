namespace ScheduleServer.Data;
using Microsoft.EntityFrameworkCore;
using ScheduleServer.Models;

    public class ShcheduleContext : DbContext
    {
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Classrooms> classrooms { get; set; }

        public ShcheduleContext(DbContextOptions<ShcheduleContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("schudele");
            entity.HasOne(s => s.users).WithMany().HasForeignKey(s => s.user_id);
            entity.HasOne(s => s.subject).WithMany().HasForeignKey(s => s.subject_id);
            entity.HasOne(s => s.group).WithMany().HasForeignKey(s => s.group_id);
            entity.HasIndex(s => new { s.day_of_week, s.type });
            entity.HasOne(s => s.room).WithMany().HasForeignKey(s => s.classroom_id);
            entity.Property(p => p.start_time).HasColumnName("start_time");
            entity.Property(p => p.end_time).HasColumnName("end_time");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("subjects");
            entity.Property(s => s.name).IsRequired().HasMaxLength(100);
            entity.HasIndex(s => s.name);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasOne(u => u.role).WithMany().HasForeignKey(u => u.role_id);
            entity.Property(u => u.name).IsRequired().HasMaxLength(100);

        });

       
        }
    }
