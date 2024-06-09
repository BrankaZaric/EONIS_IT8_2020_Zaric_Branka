using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data.KorisnikData;
using VPDecijeIgracke.Models.KorisnikModel;
using BCrypt.Net;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class KorisnikController : Controller
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepository korisnikRepository, IMapper mapper)
        {
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        [Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<List<KorisnikDTO>>> GetAllKorisnici()
        {
            var korisnici = await korisnikRepository.GetAllKorisnici();

            if (korisnici == null || korisnici.Count == 0)
            {
                return NoContent();
            }

            return Ok(korisnici);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{korisnikId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<KorisnikDTO>> GetKorisnikById(int korisnikId)
        {
            var korisnik = await korisnikRepository.GetKorisnikById(korisnikId);

            if (korisnik == null)
            {
                return NotFound("Korisnik sa datim ID-jem nije pronadjen.");
            }

            return Ok(mapper.Map<KorisnikDTO>(korisnik));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        public async Task<ActionResult<KorisnikConfirmation>> CreateKorisnik([FromBody] KorisnikCreationDTO korisnik)
        {
            try
            {
                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka);
                Korisnik korisnik1 = mapper.Map<Korisnik>(korisnik);

                var (isValid, errorMessage) = await ValidateKorisnik(korisnik1);

                if (!isValid)
                {
                    return BadRequest(errorMessage);
                }

                KorisnikConfirmation confirmation = await korisnikRepository.CreateKorisnik(korisnik1);
                await korisnikRepository.SaveChanges();

                return Ok(mapper.Map<KorisnikConfirmation>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error:" + ex);
            }
        }

        private async Task<(bool, string)> ValidateKorisnik(Korisnik korisnik)
        {
            List<Korisnik> korisnici = await korisnikRepository.GetAllKorisnici();

            bool korisnickoImePostoji = korisnici.Any(k => k.KorisnickoIme == korisnik.KorisnickoIme);
            bool emailPostoji = korisnici.Any(k => k.Email == korisnik.Email);
            bool lozinkaPostoji = korisnici.Any(k => k.Lozinka == korisnik.Lozinka);
            bool lozinkaDuzina = korisnik.Lozinka.Length >= 8; // Provera duzine lozinke

            if (korisnickoImePostoji)
            {
                return (false, "Korisnicko ime vec postoji.");
            }

            if (emailPostoji)
            {
                return (false, "Email vec postoji.");
            }

            if (lozinkaPostoji)
            {
                return (false, "Lozinka vec postoji.");
            }

            if (!lozinkaDuzina)
            {
                return (false, "Lozinka administratora mora imati najmanje 8 karaktera.");
            }

            return (true, "Validacija uspesna.");
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        public async Task<ActionResult<KorisnikDTO>> UpdateKorisnik(KorisnikUpdateDTO korisnikUpdateDTO)
        {
            try
            {
                // Proveriti da li postoji Korisnik koji želimo da ažuriramo
                var oldKorisnik = await korisnikRepository.GetKorisnikById(korisnikUpdateDTO.KorisnikID);
                if (oldKorisnik == null)
                {
                    return NotFound("Korisnik koji želite ažurirati ne postoji.");
                }

                // Mapiraj samo potrebna polja iz DTO u entitet
                mapper.Map(korisnikUpdateDTO, oldKorisnik);

                await korisnikRepository.SaveChanges();

                // Mapiraj ažurirani entitet natrag u DTO za vraćanje kao odgovor
                var updatedKorisnikDTO = mapper.Map<KorisnikDTO>(oldKorisnik);

                return Ok(updatedKorisnikDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error:" + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{korisnikId}")]
        [Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult> DeleteKorisnik(int korisnikId)
        {
            try
            {
                var korisnik = await korisnikRepository.GetKorisnikById(korisnikId);

                if (korisnik == null)
                {
                    return NotFound("Korisnik sa datim ID-jem kog zelite obrisati ne postoji.");
                }

                await korisnikRepository.DeleteKorisnik(korisnikId);
                await korisnikRepository.SaveChanges();

                return Ok("Korisnik uspesno obrisan.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }

    }
}
