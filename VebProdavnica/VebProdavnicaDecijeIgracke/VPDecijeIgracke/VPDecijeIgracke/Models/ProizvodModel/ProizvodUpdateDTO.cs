using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Models.ProizvodModel
{
    public class ProizvodUpdateDTO
    {
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public int Kolicina { get; set; }
        public string SlikaURL { get; set; }

        //strani kljucevi
        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get; set; }


       public int AdminID { get; set; }
       public Administrator Administrator { get; set; }
    }
}
