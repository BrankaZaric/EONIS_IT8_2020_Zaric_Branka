using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Data.KategorijaProizvodaData
{
    public class KategorijaRepository : IKategorijaRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public KategorijaRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Kategorija>> GetAllKategorije()
        {
            return await context.Kategorija.ToListAsync();
        }

        public async Task<Kategorija> GetKategorijaById(int kategorijaID)
        {
            return await context.Kategorija.FirstOrDefaultAsync(k => k.KategorijaID == kategorijaID);
        }

        public async Task<KategorijaConfirmation> CreateKategorija(Kategorija kategorija)
        {
            var createdEntity = await context.AddAsync(kategorija);
            await context.SaveChangesAsync();
            return mapper.Map<KategorijaConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateKategorija(Kategorija kategorija)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteKategorija(int kategorijaID)
        {
            var kategorija = await GetKategorijaById(kategorijaID);
            context.Kategorija.Remove(kategorija);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

    }
}
