using VPDecijeIgracke.Data.Specifications;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.ProizvodData
{
    public interface IProizvodRepository
    {
        Task<List<Proizvod>> GetAllProizvodi();
        Task<Proizvod> GetProizvodById(int proizvodID);

        /**/
        Task<IReadOnlyList<Proizvod>> GetProizvodAsync();
        Task<IReadOnlyCollection<Kategorija>> GetKategorijaAsync();
        /**/

        Task<ProizvodConfirmation> CreateProizvod(Proizvod proizvod);
        Task UpdateProizvod(Proizvod proizvod);
        Task DeleteProizvod(int proizvodID);
        Task<bool> SaveChanges();

        //pretraga proizvoda po nazivu
        Task<List<Proizvod>> SearchProizvodiByNaziv(string search);

        //pronalazak proizvoda u odredjenom opsegu cene
        Task<List<Proizvod>> GetProizvodiByCenaProizvoda(decimal? minCena, decimal? maxCena);

    }
}
