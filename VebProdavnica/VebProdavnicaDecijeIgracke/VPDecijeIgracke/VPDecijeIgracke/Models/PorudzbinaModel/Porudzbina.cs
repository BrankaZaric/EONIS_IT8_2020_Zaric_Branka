using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VPDecijeIgracke.Models.KorisnikModel;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;
using System.Text.Json.Serialization;

namespace VPDecijeIgracke.Models.PorudzbinaModel
{
    public class Porudzbina
    {
        public Porudzbina()
        {
            Status = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PorudzbinaID { get; set; }

        [Required(ErrorMessage = "Datum porudzbine je obavezan.")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Status porudzbine je obavezan.")]
        [StringLength(20)]
        public string Status { get; set; }

        public decimal Iznos { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        //strani kljucevi
        public int KorisnikID { get; set; }
        [ForeignKey("KorisnikID")]
        public Korisnik Korisnik { get; set; }

        [JsonIgnore]
        public ICollection<StavkaPorudzbine> StavkePorudzbine { get; set; }
    }
}
