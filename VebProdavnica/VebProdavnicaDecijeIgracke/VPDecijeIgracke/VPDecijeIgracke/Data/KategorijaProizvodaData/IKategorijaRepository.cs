using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Data.KategorijaProizvodaData
{
    public interface IKategorijaRepository
    {
        Task<List<Kategorija>> GetAllKategorije();
        Task<Kategorija> GetKategorijaById(int kategorijaID);
        Task<KategorijaConfirmation> CreateKategorija(Kategorija kategorija);
        Task UpdateKategorija(Kategorija kategorija);
        Task DeleteKategorija(int kategorijaID);
        Task<bool> SaveChanges();
    }
}
