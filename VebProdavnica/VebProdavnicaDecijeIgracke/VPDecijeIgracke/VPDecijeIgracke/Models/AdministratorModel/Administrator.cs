using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VPDecijeIgracke.Models.AdministratorModel
{
    public class Administrator
    {
        //inicijalizacija svih svojstava kroz konstruktor
        //vrednosti ne mogu biti nullable
        public Administrator()
        {
            ImeAdmin = string.Empty;
            PrezimeAdmin = string.Empty;
            EmailAdmin = string.Empty;
            KorisnickoImeAdmin = string.Empty;
            LozinkaAdmin = string.Empty;
            AdresaAdmin = string.Empty;
            TelefonAdmin = string.Empty;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //automatsko generisanje kljuca 
        public int AdminID { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(20)]
        public string ImeAdmin { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(20)]
        public string PrezimeAdmin { get; set; }

        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Email adresa nije validna.")]
        public string EmailAdmin { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [StringLength(20)]
        public string KorisnickoImeAdmin { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [MinLength(8, ErrorMessage = "Lozinka mora imati najmanje 8 karaktera.")]
        public string LozinkaAdmin { get; set; }

        public string AdresaAdmin { get; set; }

        public string TelefonAdmin { get; set; }

    }
}
