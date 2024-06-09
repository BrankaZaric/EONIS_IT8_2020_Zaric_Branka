using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VPDecijeIgracke.Models.KorisnikModel
{
    public class Korisnik
    {
        //inicijalizacija svih svojstava kroz konstruktor
        //vrednosti ne mogu biti nullable
        public Korisnik()
        {
            Ime = string.Empty;
            Prezime = string.Empty;
            Email = string.Empty;
            KorisnickoIme = string.Empty;
            Lozinka = string.Empty;
            Adresa = string.Empty;
            Telefon = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //automatsko generisanje kljuca 
        public int KorisnikID { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(20)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(20)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Email adresa nije validna.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [StringLength(20)]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [MinLength(8, ErrorMessage = "Lozinka mora imati najmanje 8 karaktera.")]
        public string Lozinka { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

    }
}
