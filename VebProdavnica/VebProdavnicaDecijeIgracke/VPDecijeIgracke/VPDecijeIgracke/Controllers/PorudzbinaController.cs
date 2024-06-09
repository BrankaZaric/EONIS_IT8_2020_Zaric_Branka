using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data.KorisnikData;
using VPDecijeIgracke.Data.PorudzbinaData;
using VPDecijeIgracke.Models.PorudzbinaModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/porudzbina")]
    [Produces("application/json", "application/xml")]
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper mapper;

        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository,IKorisnikRepository korisnikRepository, IMapper mapper)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<List<PorudzbinaDTO>>> GetAllPorudzbine()
        {
            var porudzbine = await porudzbinaRepository.GetAllPorudzbine();

            if (porudzbine == null || porudzbine.Count == 0)
            {
                return NoContent();
            }

            return Ok(porudzbine);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{porudzbinaId}")] 
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<PorudzbinaDTO>> GetPorudzbinaById(int porudzbinaId)
        {
            var porudzbina = await porudzbinaRepository.GetPorudzbinaById(porudzbinaId);

            if (porudzbina == null)
            {
                return NotFound("Porudzbina sa datim ID-jem nije pronadjen.");
            }

            return Ok(mapper.Map<PorudzbinaDTO>(porudzbina));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.KorisnikPolicy)]
        public async Task<ActionResult<PorudzbinaConfirmation>> CreatePorudzbina([FromBody] PorudzbinaCreationDTO porudzbina)
        {
            try
            {
                Porudzbina porudzbina1 = mapper.Map<Porudzbina>(porudzbina);

                /*var (isValid, errorMessage) = await ValidateKorisnik(korisnik1);

                if (!isValid)
                {
                    return BadRequest(errorMessage);
                }*/

                PorudzbinaConfirmation confirmation = await porudzbinaRepository.CreatePorudzbina(porudzbina1);
                await porudzbinaRepository.SaveChanges();

                return Ok(mapper.Map<PorudzbinaConfirmation>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error:" + ex);
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<PorudzbinaDTO>> UpdatePorudzbina(PorudzbinaUpdateDTO porudzbina)
        {
            try
            {
                //Proveriti da li postoji Porudzbina koju zelimo da azuriramo
                var oldPorudzbina = await porudzbinaRepository.GetPorudzbinaById(porudzbina.PorudzbinaID);

                if (oldPorudzbina == null)
                {
                    return NotFound("Porudzbina koju zelite azurirati ne postoji.");
                }

                Porudzbina porudzbinaEntity = mapper.Map<Porudzbina>(porudzbina);

                mapper.Map(porudzbinaEntity, oldPorudzbina);

                await porudzbinaRepository.SaveChanges();
                return Ok(mapper.Map<PorudzbinaDTO>(oldPorudzbina));
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
        [HttpDelete("{porudzbinaId}")]
        public async Task<ActionResult> DeletePorudzbina(int porudzbinaId)
        {
            try
            {
                var porudzbina = await porudzbinaRepository.GetPorudzbinaById(porudzbinaId);

                if (porudzbina == null)
                {
                    return NotFound("Porudzbina sa datim ID-jem koju zelite obrisati ne postoji.");
                }

                await porudzbinaRepository.DeletePorudzbina(porudzbinaId);
                await porudzbinaRepository.SaveChanges();

                return Ok("Porudzbina uspesno obrisana.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }

        [HttpGet("korisnik/{korisnikId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        [Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<List<PorudzbinaDTO>>> GetPorudzbineZaKorisnika(int korisnikId)
        {

            var porudzbineZaKorisnika = await porudzbinaRepository.GetPorudzbineZaKorisnika(korisnikId);

            if (porudzbineZaKorisnika == null || porudzbineZaKorisnika.Count == 0)
            {
                return NotFound("Za datog korisnika ne postoji nijedna porudzbina");
            }

            return Ok(porudzbineZaKorisnika.Select(p => mapper.Map<PorudzbinaDTO>(p)).ToList());
        }

        [HttpGet("placene")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<PorudzbinaDTO>>> GetPlacenePorudzbine()
        {
            var placenePorudzbine = await porudzbinaRepository.GetPlacenePorudzbine();

            if (placenePorudzbine == null || placenePorudzbine.Count == 0)
            {
                return NoContent();
            }

            return Ok(placenePorudzbine.Select(p => mapper.Map<PorudzbinaDTO>(p)).ToList());
        }
    }
}
