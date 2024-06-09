namespace VPDecijeIgracke.Models.ProizvodModel
{
    public class ProizvodCreationDTO
    {
        public string NazivProizvoda { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public int Kolicina { get; set; }
        public string SlikaURL { get; set; }

        //strani kljucevi
        public int KategorijaID { get; set; }

        public int AdminID { get; set; }
    }
}
