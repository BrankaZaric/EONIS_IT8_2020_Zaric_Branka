using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Models.PorudzbinaModel
{
    public class PorudzbinaUpdateDTO
    {
        public int PorudzbinaID { get; set; }
        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public decimal Iznos { get; set; }


        //strani kljucevi
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
