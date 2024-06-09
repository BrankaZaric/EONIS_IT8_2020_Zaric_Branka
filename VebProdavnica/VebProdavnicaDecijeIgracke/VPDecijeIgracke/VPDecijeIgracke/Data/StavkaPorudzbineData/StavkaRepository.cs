using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Models.ProizvodModel;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Data.StavkaPorudzbineData
{
    public class StavkaRepository : IStavkaRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public StavkaRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<StavkaPorudzbine>> GetAllStavke()
        {
            var stavke = await context.StavkaPorudzbine
                            .Include(s => s.Proizvod) // Ukljucuje podatke o proizvodu
                            .Include(s => s.Porudzbina) // Ukljucuje podatke o porudzbini
                            .ToListAsync();

            return stavke;
        }

        public async Task<StavkaPorudzbine> GetStavkaById(int stavkaID)
        {
            return await context.StavkaPorudzbine
                                .Include(s => s.Proizvod) 
                                .Include(s => s.Porudzbina)
                                .FirstOrDefaultAsync(st => st.StavkaID == stavkaID);
        }

        public async Task<StavkaPorudzbineConfirmation> CreateStavka(StavkaPorudzbine stavka)
        {
            var createdEntity = await context.AddAsync(stavka);
            await context.SaveChangesAsync();
            return mapper.Map<StavkaPorudzbineConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateStavka(StavkaPorudzbine stavka)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteStavka(int stavkaID)
        {
            var stavka = await GetStavkaById(stavkaID);
            context.StavkaPorudzbine.Remove(stavka);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }


    }
}
