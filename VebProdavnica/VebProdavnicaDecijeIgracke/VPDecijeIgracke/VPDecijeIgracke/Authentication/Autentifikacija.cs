
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;
using VPDecijeIgracke.Data.AdministratorData;
using VPDecijeIgracke.Data.KorisnikData;

namespace VPDecijeIgracke.Authentication
{
    [ApiController]
    [Route("api/autentifikacija")]
    [EnableCors]
    [Produces("application/json", "application/xml")]
    public class Autentifikacija : Controller
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IAdministratorRepository administratorRepository;
        private readonly IJwt jwt;

        public Autentifikacija(IKorisnikRepository korisnikRepository, IAdministratorRepository administratorRepository, IJwt jwt)
        {
            this.korisnikRepository = korisnikRepository;
            this.administratorRepository = administratorRepository;
            this.jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            // Provera u tabeli Korisnik
            var korisnik = await korisnikRepository.GetKorisnikByKorisnickoIme(model.KorisnickoIme);

            if (korisnik != null)
            {
                if(BCrypt.Net.BCrypt.Verify(model.Lozinka, korisnik.Lozinka))
                {
                    // Generisanje jwt tokena za korisnika
                    var token = jwt.KorisnikToken(korisnik);
                    return Ok(new { token }); // Vraćanje tokena kao JSON objekat
                }
                
            }
            else
            {
                // Provera u tabeli Administrator
                var administrator = await administratorRepository.GetKAdministratorByKorisnickoIme(model.KorisnickoIme);

                if (administrator != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(model.Lozinka, administrator.LozinkaAdmin))
                    {
                        // Generisanje jwt tokena za korisnika
                        var token = jwt.AdministratorToken(administrator);
                        return Ok(new { token }); 
                    }
                    
                }
            }

            // Ako korisnik nije pronađen ili lozinka nije ispravna
            return Unauthorized("Korisnik nije pronadjen ili lozinka nije ispravna!");

        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return Unauthorized("Nevalidan token");

            // Pokušaj da pročitaš custom claim "korisnickoIme" i "uloga"
            var korisnickoImeClaim = identity.FindFirst("korisnickoIme")?.Value;
            var ulogaClaim = identity.FindFirst("uloga")?.Value;

            if (korisnickoImeClaim == null || ulogaClaim == null)
                return Unauthorized("Nevalidan token");

            if (ulogaClaim == "korisnik")
            {
                var korisnik = await korisnikRepository.GetKorisnikByKorisnickoIme(korisnickoImeClaim);
                if (korisnik != null)
                {
                    Console.WriteLine("Korisnik pronađen: " + korisnik.KorisnickoIme);
                    return Ok(korisnik);
                }
            }
            else if (ulogaClaim == "administrator")
            {
                var administrator = await administratorRepository.GetKAdministratorByKorisnickoIme(korisnickoImeClaim);
                if (administrator != null)
                {
                    Console.WriteLine("Administrator pronađen: " + administrator.KorisnickoImeAdmin);
                    return Ok(administrator);
                }
            }

            return Unauthorized("Korisnik nije pronađen");
        }


    }

}

