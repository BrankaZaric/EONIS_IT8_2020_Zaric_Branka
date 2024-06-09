using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Models.ProizvodModel
{
    public class Proizvod
    {
        public Proizvod()
        {
            NazivProizvoda = string.Empty;
            Opis = string.Empty;
            SlikaURL = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProizvodID { get; set; }

        [Required(ErrorMessage = "Naziv proizvoda je obavezan.")]
        [StringLength(100)]
        public string NazivProizvoda { get; set; }

        [Required(ErrorMessage = "Opis proizvoda je obavezan.")]
        [StringLength(2000)]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Cena proizvoda je obavezan.")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Kolicina proizvoda je obavezna.")]
        public int Kolicina { get; set; }

        [Required(ErrorMessage = "Slika proizvoda je obavezna.")]
        [StringLength(2000)]
        public string SlikaURL { get; set; }

        //strani kljucevi
        public int KategorijaID { get; set; }
        [ForeignKey("KategorijaID")]
        public virtual Kategorija Kategorija { get; set; }

        public int AdminID { get; set; }

        // Veza ka Administratoru
        [ForeignKey("AdminID")]
        public virtual Administrator Administrator { get; set; }


    }
}
