using Microsoft.EntityFrameworkCore;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;
using VPDecijeIgracke.Models.KorisnikModel;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Context
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }


        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<StavkaPorudzbine> StavkaPorudzbine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("WebShop"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indeksiranje za jedinstvenost 'Email', 'KorisnickoIme', 'Lozinka'
            modelBuilder.Entity<Korisnik>()
                .HasIndex(k => k.Email)
                .IsUnique();

            modelBuilder.Entity<Korisnik>()
                .HasIndex(k => k.KorisnickoIme)
                .IsUnique();

            modelBuilder.Entity<Korisnik>()
                .HasIndex(k => k.Lozinka)
                .IsUnique();

            // Indeksiranje za jedinstvenost 'Email', 'KorisnickoIme', 'Lozinka'
            modelBuilder.Entity<Administrator>()
                .HasIndex(k => k.EmailAdmin)
                .IsUnique();

            modelBuilder.Entity<Administrator>()
                .HasIndex(k => k.KorisnickoImeAdmin)
                .IsUnique();

            modelBuilder.Entity<Administrator>()
                .HasIndex(k => k.LozinkaAdmin)
                .IsUnique();

            // Indeksiranje za jedinstvenost 'NazivKategorije'
            modelBuilder.Entity<Kategorija>()
                .HasIndex(k => k.NazivKategorije)
                .IsUnique();

            // Indeksiranje za jedinstvenost 'NazivProizvoda', 'SlikaURL'
            modelBuilder.Entity<Proizvod>()
                .HasIndex(k => k.NazivProizvoda)
                .IsUnique();

            modelBuilder.Entity<Proizvod>()
                .HasIndex(k => k.SlikaURL)
                .IsUnique();
        }
    }
}
