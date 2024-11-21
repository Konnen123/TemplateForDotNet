using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Appointment>(

                    entity =>
                    {
                        entity.ToTable("appointments");
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Id)
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()")
                        .ValueGeneratedOnAdd();
                        entity.Property(entity => entity.Type).IsRequired().HasMaxLength(50);
                        entity.Property(entity => entity.Description).IsRequired().HasMaxLength(100);
                        entity.Property(entity => entity.StartTime).IsRequired().HasMaxLength(20);
                        entity.Property(entity => entity.EndTime).IsRequired().HasMaxLength(20);
                    }
                );
        }

    }
}
