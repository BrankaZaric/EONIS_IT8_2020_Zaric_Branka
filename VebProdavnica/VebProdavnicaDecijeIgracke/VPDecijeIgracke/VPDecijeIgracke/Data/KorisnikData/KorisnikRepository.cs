using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Data.KorisnikData
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public KorisnikRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //async - asinhrone metode: one koje se mogu izvrsavati na odvojenom thread-u
        //omogucava pozivajucem kodu da se nastavi dok asihrona operacija traje (izbegava se blokiranje glavnog thread-a)
        //await - asihrono cekanje izvrsenja asihrone operacije
        public async Task<List<Korisnik>> GetAllKorisnici()
        {
            return await context.Korisnik.ToListAsync();

        }

        public async Task<Korisnik> GetKorisnikById(int korisnikID)
        {
            return await context.Korisnik.FirstOrDefaultAsync(k => k.KorisnikID == korisnikID);
        }

        public async Task<KorisnikConfirmation> CreateKorisnik(Korisnik korisnik)
        {
            var createdEntity = await context.AddAsync(korisnik);
            await context.SaveChangesAsync();
            return mapper.Map<KorisnikConfirmation>(createdEntity.Entity);
        }
        public async Task UpdateKorisnik(Korisnik korisnik)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteKorisnik(int korisnikID)
        {
            var korisnik = await GetKorisnikById(korisnikID);
            context.Korisnik.Remove(korisnik);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        //metoda za autentifikaciju
        public async Task<Korisnik> GetKorisnikByKorisnickoIme(string korisnickoIme)
        {
            return await context.Korisnik.FirstOrDefaultAsync(k => k.KorisnickoIme == korisnickoIme);
        }

        public Korisnik GetByKorisnickoIme(string KorisnickoIme)
        {
            return  context.Korisnik.FirstOrDefault(k => k.KorisnickoIme == KorisnickoIme);
        }
    }
}
