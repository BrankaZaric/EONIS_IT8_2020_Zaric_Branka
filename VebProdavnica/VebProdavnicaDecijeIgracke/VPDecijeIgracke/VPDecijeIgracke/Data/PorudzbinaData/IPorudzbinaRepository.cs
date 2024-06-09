using VPDecijeIgracke.Models.PorudzbinaModel;

namespace VPDecijeIgracke.Data.PorudzbinaData
{
    public interface IPorudzbinaRepository
    {
        Task<List<Porudzbina>> GetAllPorudzbine();
        Task<Porudzbina> GetPorudzbinaById(int porudzbinaID);
        Task<PorudzbinaConfirmation> CreatePorudzbina(Porudzbina porudzbina);
        Task UpdatePorudzbina(Porudzbina porudzbina);
        Task DeletePorudzbina(int porudzbinaID);
        Task<bool> SaveChanges();

        //vracanje svih porudzbine za odredjenog korisnika
        Task<List<Porudzbina>> GetPorudzbineZaKorisnika(int korisnikId);

        // Metoda koja vraća sve porudzbine kod kojih je status "placena"
        Task<List<Porudzbina>> GetPlacenePorudzbine();
    }
}
