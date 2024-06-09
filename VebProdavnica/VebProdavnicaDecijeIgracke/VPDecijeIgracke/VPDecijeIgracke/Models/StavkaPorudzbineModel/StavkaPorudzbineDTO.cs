using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Models.StavkaPorudzbineModel
{
    public class StavkaPorudzbineDTO
    {
        public int StavkaID { get; set; }
        public decimal CenaStavka { get; set; }
        public int KolicinaStavka { get; set; }

        //strani kljucevi
        public int ProizvodID { get; set; }
        public Proizvod Proizvod { get; set; }

        public int PorudzbinaID { get; set; }
        public Porudzbina Porudzbina { get; set; }
    }
}
