using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data.AdministratorData;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/administrator")]
    [Produces("application/json", "application/xml")]
    //[Authorize(Policy = IdentityData.AdministratorPolicy)]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorRepository administratorRepository;
        private readonly IMapper mapper;

        public AdministratorController(IAdministratorRepository administratorRepository, IMapper mapper)
        {
            this.administratorRepository = administratorRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<AdministratorDTO>>> GetAllAdministratori()
        {
            var administratori = await administratorRepository.GetAllAdministratori();

            if (administratori == null || administratori.Count == 0)
            {
                return NoContent();
            }

            return Ok(administratori);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{administratorId}")] 
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<AdministratorDTO>> GetAdministratorById(int administratorId)
        {
            var administrator = await administratorRepository.GetAdministratorById(administratorId);

            if (administrator == null)
            {
                return NotFound("Administrator sa datim ID-jem ne postoji.");
            }

            return Ok(mapper.Map<AdministratorDTO>(administrator));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("lozinka/{administratorId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<AdministratorLozinkaDTO>> GetAdministratorLozinkaById(int administratorId)
        {
            var administrator = await administratorRepository.GetAdministratorLozinkaById(administratorId);

            if (administrator == null)
            {
                return NotFound("Administrator sa datim ID-jem ne postoji.");
            }

            return Ok(mapper.Map<AdministratorLozinkaDTO>(administrator));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<AdministratorConfirmation>> CreateAdministrator([FromBody] AdministratorCreationDTO administrator)
        {
            try
            {
                administrator.LozinkaAdmin = BCrypt.Net.BCrypt.HashPassword(administrator.LozinkaAdmin);
                Administrator administrator1 = mapper.Map<Administrator>(administrator);

                var (isValid, errorMessage) = await ValidateAdministrator(administrator1);

                if (!isValid)
                {
                    return BadRequest(errorMessage);
                }

                AdministratorConfirmation confirmation = await administratorRepository.CreateAdministrator(administrator1);
                await administratorRepository.SaveChanges();

                return Ok(mapper.Map<AdministratorConfirmation>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error:" + ex);
            }
        }

        private async Task<(bool, string)> ValidateAdministrator(Administrator admin)
        {
            List<Administrator> administratori = await administratorRepository.GetAllAdministratori();

            bool korisnickoImePostoji = administratori.Any(k => k.KorisnickoImeAdmin == admin.KorisnickoImeAdmin);
            bool emailPostoji = administratori.Any(k => k.EmailAdmin == admin.EmailAdmin);
            bool lozinkaPostoji = administratori.Any(k => k.LozinkaAdmin == admin.LozinkaAdmin);
            bool lozinkaDuzina = admin.LozinkaAdmin.Length >= 8; // Provera duzine lozinke

            if (korisnickoImePostoji)
            {
                return (false, "Korisnicko ime administratora vec postoji.");
            }

            if (emailPostoji)
            {
                return (false, "Email administratora vec postoji.");
            }

            if (lozinkaPostoji)
            {
                return (false, "Lozinka administratora vec postoji.");
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
        public async Task<ActionResult<AdministratorDTO>> UpdateAdministrator(AdministratorUpdateDTO administrator)
        {
            try
            {
                //Proveriti da li postoji Administrator koji zelimo da azuriramo
                var oldAdministrator = await administratorRepository.GetAdministratorById(administrator.AdminID);
                if (oldAdministrator == null)
                {
                    return NotFound("Administrator koga zelite azurirati ne postoji.");
                }

                Administrator adminEntity = mapper.Map<Administrator>(administrator);

                mapper.Map(adminEntity, oldAdministrator);

                await administratorRepository.SaveChanges();
                return Ok(mapper.Map<AdministratorDTO>(oldAdministrator));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error:" + ex);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{administratorId}")]
        public async Task<ActionResult> DeleteAdministrator(int administratorId)
        {
            try
            {
                var administrator = await administratorRepository.GetAdministratorById(administratorId);

                if (administrator == null)
                {
                    return NotFound("Administrator sa datim ID-jem koga zelite obrisati ne postoji.");
                }

                await administratorRepository.DeleteAdministrator(administratorId);
                await administratorRepository.SaveChanges();

                return Ok("Administrator uspesno obrisan.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }
    }
}
