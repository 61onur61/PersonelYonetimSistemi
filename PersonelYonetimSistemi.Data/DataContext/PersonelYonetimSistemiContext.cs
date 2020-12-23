using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonelYonetimSistemi.Data.Models;

namespace PersonelYonetimSistemi.Data.DataContext
{
    public class PersonelYonetimSistemiContext:IdentityDbContext
    {
        public PersonelYonetimSistemiContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Personels> Personels { get; set; }

        public DbSet<PersonelIzin> PersonelIzins { get; set; }

        public DbSet<PersonelIzinTipi> personelIzinTipis { get; set; }

        public DbSet<PersonelIzinOnay> PersonelIzinOnays { get; set; }

        public DbSet<PersonelIsEmirleri> PersonelIsEmirleris { get; set; }

        public DbSet<IsEmriDurumlari> IsEmriDurumlaris { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Personels>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Personels>()
                .Property(p => p.IsAdmin)
                .HasDefaultValue(false);

            base.OnModelCreating(builder);
        }
    }
}
