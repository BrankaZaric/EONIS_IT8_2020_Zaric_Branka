namespace VPDecijeIgracke.Models.PorudzbinaModel
{
    public class PorudzbinaCreationDTO
    {
        public DateTime Datum { get; set; }

        public string Status { get; set; }

        public decimal Iznos { get; set; }


        //strani kljucevi
        public int KorisnikID { get; set; }
    }
}
