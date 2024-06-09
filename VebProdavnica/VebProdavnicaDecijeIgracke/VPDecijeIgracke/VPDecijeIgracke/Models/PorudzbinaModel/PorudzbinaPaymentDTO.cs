using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Models.PorudzbinaModel
{
    public class PorudzbinaPaymentDTO
    {
        public int PorudzbinaID { get; set; }
        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public decimal Iznos { get; set; }
        public int KorisnikID { get; set; }

        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
