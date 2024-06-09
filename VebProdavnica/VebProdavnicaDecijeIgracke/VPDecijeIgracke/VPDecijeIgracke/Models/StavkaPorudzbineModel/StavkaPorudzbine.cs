using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Models.StavkaPorudzbineModel
{
    public class StavkaPorudzbine
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StavkaID { get; set; }
        public decimal CenaStavka { get; set; }

        public int KolicinaStavka { get; set; }

        //strani kljucevi
        public int ProizvodID { get; set; }
        [ForeignKey("ProizvodID")]
        public Proizvod Proizvod { get; set; }

        public int PorudzbinaID { get; set; }
        [ForeignKey("PorudzbinaID")]
        public Porudzbina Porudzbina { get; set; }
    }
}
