using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Data.AdministratorData
{
    public interface IAdministratorRepository
    {
        Task<List<Administrator>> GetAllAdministratori();
        Task<Administrator> GetAdministratorById(int administratorID);
        Task<AdministratorConfirmation> CreateAdministrator(Administrator administrator);
        Task UpdateAdministrator(Administrator administrator);
        Task DeleteAdministrator(int administratorID);
        Task<bool> SaveChanges();

        Task<Administrator> GetKAdministratorByKorisnickoIme(string korisnickoImeAdmin);

        Task<Administrator> GetAdministratorLozinkaById(int administratorID);
    }
}
