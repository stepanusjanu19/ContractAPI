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

            modelBuilder.Entity<Kontrak>()
                        .ToTable("kontrak")
                        .HasMany(k => k.JadwalAngsurans)
                        .WithOne(j => j.Kontrak)
                        .HasForeignKey(j => j.KontrakNo);

            modelBuilder.Entity<JadwalAngsuran>()
                    .ToTable("jadwal_angsuran");
        }
    }

}