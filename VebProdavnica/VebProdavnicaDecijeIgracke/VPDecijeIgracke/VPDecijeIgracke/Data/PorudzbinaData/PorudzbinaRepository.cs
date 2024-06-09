using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Models;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.PorudzbinaData
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public PorudzbinaRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Porudzbina>> GetAllPorudzbine()
        {
            var porudzbine = await context.Porudzbina
                            .Include(p => p.Korisnik) // Ukljucuje podatke o korisniku
                            .ToListAsync();

            return porudzbine;
        }

        public async Task<Porudzbina> GetPorudzbinaById(int porudzbinaID)
        {
            return await context.Porudzbina
                                .Include(p => p.Korisnik)
                                .FirstOrDefaultAsync(po => po.PorudzbinaID == porudzbinaID);
        }

        public async Task<PorudzbinaConfirmation> CreatePorudzbina(Porudzbina porudzbina)
        {
            var createdEntity = await context.AddAsync(porudzbina);
            await context.SaveChangesAsync();
            return mapper.Map<PorudzbinaConfirmation>(createdEntity.Entity);
        }

        public async Task UpdatePorudzbina(Porudzbina porudzbina)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeletePorudzbina(int porudzbinaID)
        {
            var porudzbina = await GetPorudzbinaById(porudzbinaID);
            context.Porudzbina.Remove(porudzbina);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        //vracanje svih porudzbine za odredjenog korisnika
        public async Task<List<Porudzbina>> GetPorudzbineZaKorisnika(int korisnikId)
        {
            return await context.Porudzbina.Where(p => p.KorisnikID == korisnikId).ToListAsync();
        }

        public async Task<List<Porudzbina>> GetPlacenePorudzbine()
        {
            return await context.Porudzbina.Where(p => p.Status == "Placena").ToListAsync();
        }

    }
}
