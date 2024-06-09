using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Models.PorudzbinaModel
{
    public class PorudzbinaDTO
    {
        public int PorudzbinaID { get; set; }
        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public decimal Iznos { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }

        //strani kljucevi
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        
    }
}
