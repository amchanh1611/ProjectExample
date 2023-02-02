using Humanizer;
using Microsoft.EntityFrameworkCore;
using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Media> medias { get; set; }
        public DbSet<Schedule> schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>(entityTypeBuilder =>
            {
                entityTypeBuilder.ToTable(nameof(Media).Underscore());
                entityTypeBuilder.HasKey(x => x.Id);
                entityTypeBuilder.Property(x => x.Name).HasColumnType("varchar(255)");
                entityTypeBuilder.Property(x => x.ContentType).HasColumnType("varchar(100)");
                entityTypeBuilder.Property(x => x.File).HasColumnType("varchar(2048)");
                entityTypeBuilder.Property(x => x.Status).HasColumnType("tinyint");
            });
            modelBuilder.Entity<Schedule>(entityTypeBuilder =>
            {
                entityTypeBuilder.ToTable(nameof(Schedule).Underscore());
                entityTypeBuilder.HasKey(x => x.Id);
                entityTypeBuilder.HasOne<Media>(o => o.Media)
                .WithMany(w => w.Schedules)
                .HasForeignKey(f => f.MediaId);
                entityTypeBuilder.Property(x => x.Description).HasColumnType("text");
            });
        }
    }
}