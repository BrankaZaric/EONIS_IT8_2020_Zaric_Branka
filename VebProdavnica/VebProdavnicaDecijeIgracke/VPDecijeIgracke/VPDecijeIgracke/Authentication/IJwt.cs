using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Authentication
{
    public interface IJwt
    {
        string KorisnikToken(Korisnik korisnik);
        string AdministratorToken(Administrator administrator);
    }
}
