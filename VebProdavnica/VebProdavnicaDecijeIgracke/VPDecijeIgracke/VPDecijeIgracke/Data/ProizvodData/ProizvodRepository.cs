using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Data.Specifications;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.ProizvodData
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;


        public ProizvodRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<Proizvod>> GetAllProizvodi()
        {
            var proizvodi = await context.Proizvod
                            .Include(p => p.Kategorija) // Ukljucuje podatke o kategoriji
                            .Include(p => p.Administrator) // Ukljucuje podatke o administratoru
                            .ToListAsync();

            return proizvodi;
        }


        public async Task<Proizvod> GetProizvodById(int proizvodID)
        {
            /* return await context.Proizvod
                                 .Include(p => p.Kategorija)
                                 .Include(p => p.Administrator)
                                 .FirstOrDefaultAsync(pr => pr.ProizvodID == proizvodID);*/

            return await context.Proizvod
                .Include(k => k.Kategorija)
                .FirstOrDefaultAsync(p => p.ProizvodID == proizvodID);
        }

        /**/
        public async Task<IReadOnlyList<Proizvod>> GetProizvodAsync()
        {
            return await context.Proizvod
                .Include(k => k.Kategorija)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Kategorija>> GetKategorijaAsync()
        {
            return await context.Kategorija.ToListAsync();
        }
        /**/

        public async Task<ProizvodConfirmation> CreateProizvod(Proizvod proizvod)
        {
            var createdEntity = await context.AddAsync(proizvod);
            await context.SaveChangesAsync();
            return mapper.Map<ProizvodConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateProizvod(Proizvod proizvod)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteProizvod(int proizvodID)
        {
            var proizvod = await GetProizvodById(proizvodID);
            context.Proizvod.Remove(proizvod);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        //pretraga proizvoda po nazivu proizvoda
        public async Task<List<Proizvod>> SearchProizvodiByNaziv(string search)
        {
            return await context.Proizvod
                                .Include(p => p.Kategorija)
                                .Include(p => p.Administrator)
                                .Where(p => p.NazivProizvoda.Contains(search))
                                .ToListAsync();
        }

        //pretraga proizvoda po ceni u odredjenom opsegu
        public async Task<List<Proizvod>> GetProizvodiByCenaProizvoda(decimal? minCena, decimal? maxCena)
        {
            // Inicijalizacija upita
            var query = context.Proizvod.AsQueryable();

            if (minCena != null || maxCena != null)
            {
                // Primenjujemo filter po ceni u odredjenom opsegu
                query = query.Where(p => (minCena == null || p.Cena >= minCena) &&
                                          (maxCena == null || p.Cena <= maxCena));
            }

            // Izvršavanje upita i vracanje rezultata
            return await query.ToListAsync();
        }

        
    }
}
