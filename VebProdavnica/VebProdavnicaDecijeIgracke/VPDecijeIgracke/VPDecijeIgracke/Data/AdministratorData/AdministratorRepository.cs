using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VPDecijeIgracke.Context;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Data.AdministratorData
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public AdministratorRepository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<Administrator>> GetAllAdministratori()
        {
            return await context.Administrator.ToListAsync();
        }

        public async Task<Administrator> GetAdministratorById(int administratorID)
        {
            return await context.Administrator.FirstOrDefaultAsync(a => a.AdminID == administratorID);
        }

        public async Task<Administrator> GetAdministratorLozinkaById(int administratorID)
        {
            return await context.Administrator.FirstOrDefaultAsync(a => a.AdminID == administratorID);
        }

        public async Task<AdministratorConfirmation> CreateAdministrator(Administrator administrator)
        {
            var createdEntity = await context.AddAsync(administrator);
            await context.SaveChangesAsync();
            return mapper.Map<AdministratorConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateAdministrator(Administrator administrator)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAdministrator(int administratorID)
        {
            var administrator = await GetAdministratorById(administratorID);
            context.Administrator.Remove(administrator);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }
       
        //metoda za autentifikaciju
        public async Task<Administrator> GetKAdministratorByKorisnickoIme(string korisnickoImeAdmin)
        {
            return await context.Administrator.FirstOrDefaultAsync(a => a.KorisnickoImeAdmin == korisnickoImeAdmin);
        }
    }
}
