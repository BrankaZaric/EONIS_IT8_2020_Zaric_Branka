using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Models.StavkaPorudzbineModel
{
    public class StavkaPorudzbineCreationDTO
    {
        public decimal CenaStavka { get; set; }
        public int KolicinaStavka { get; set; }

        //strani kljucevi
        public int ProizvodID { get; set; }

        public int PorudzbinaID { get; set; }
    }
}
