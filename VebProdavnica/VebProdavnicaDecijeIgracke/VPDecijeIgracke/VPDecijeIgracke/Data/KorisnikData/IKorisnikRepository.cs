using Org.BouncyCastle.Crypto.Macs;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Data.KorisnikData
{
    public interface IKorisnikRepository
    {
        Task<List<Korisnik>> GetAllKorisnici();
        Task<Korisnik> GetKorisnikById(int korisnikID);
        Task<KorisnikConfirmation> CreateKorisnik(Korisnik korisnik);
        Task UpdateKorisnik(Korisnik korisnik);
        Task DeleteKorisnik(int korisnikID);
        Task<bool> SaveChanges();


        Task<Korisnik> GetKorisnikByKorisnickoIme(string korisnickoIme);

        Korisnik GetByKorisnickoIme(string KorisnickoIme);
    }
}
