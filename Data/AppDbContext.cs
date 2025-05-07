using Microsoft.EntityFrameworkCore;
using ContractAPI.Models;

namespace ContractAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kontrak> Kontraks { get; set; }
        public DbSet<JadwalAngsuran> JadwalAngsurans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kontrak>().HasKey(k => k.KontrakNo);

            modelBuilder.Entity<JadwalAngsuran>().HasKey(j => new { j.KontrakNo, j.AngsuranKe });
            
            modelBuilder.Entity<Kontrak>()
                        .ToTable("kontrak");

            modelBuilder.Entity<JadwalAngsuran>()
                    .ToTable("jadwal_angsuran");

            modelBuilder.Entity<Kontrak>()
                        .HasMany(k => k.JadwalAngsurans)
                        .WithOne(j => j.Kontrak)
                        .HasForeignKey(j => j.KontrakNo);

            modelBuilder.Entity<JadwalAngsuran>()
                        .HasOne(j => j.Kontrak)
                        .WithMany(k => k.JadwalAngsurans)
                        .HasForeignKey(j => j.KontrakNo);

        }
    }

}