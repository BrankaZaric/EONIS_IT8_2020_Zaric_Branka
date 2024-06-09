using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VPDecijeIgracke.Models.KategorijaProizvodaModel
{
    public class Kategorija
    {
        public Kategorija()
        {
            NazivKategorije = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KategorijaID { get; set; }

        [Required(ErrorMessage = "NazivKategorije je obavezan.")]
        [StringLength(70)]
        public string NazivKategorije { get; set; }

    }
}
