using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data.StavkaPorudzbineData;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/stavkaPorudzbine")]
    [Produces("application/json", "application/xml")]
    public class StavkaPorudzbineController : Controller
    {
        private readonly IStavkaRepository stavkaRepository;
        private readonly IMapper mapper;

        public StavkaPorudzbineController(IStavkaRepository stavkaRepository, IMapper mapper)
        {
            this.stavkaRepository = stavkaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy + ", " + IdentityData.KorisnikPolicy)]
        public async Task<ActionResult<List<StavkaPorudzbineDTO>>> GetAllStavke()
        {
            var stavke = await stavkaRepository.GetAllStavke();

            if (stavke == null || stavke.Count == 0)
            {
                return NoContent();
            }

            return Ok(stavke);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{stavkaId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<StavkaPorudzbineDTO>> GetStavkaById(int stavkaId)
        {
            var stavka = await stavkaRepository.GetStavkaById(stavkaId);

            if (stavka == null)
            {
                return NotFound("Stavka porudzbine sa datim ID-jem nije pronadjena.");
            }

            return Ok(mapper.Map<StavkaPorudzbineDTO>(stavka));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.KorisnikPolicy)]
        public async Task<ActionResult<StavkaPorudzbineConfirmation>> CreateStavka([FromBody] StavkaPorudzbineCreationDTO stavka)
        {
            try
            {
                StavkaPorudzbine stavka1 = mapper.Map<StavkaPorudzbine>(stavka);

                StavkaPorudzbineConfirmation confirmation = await stavkaRepository.CreateStavka(stavka1);
                await stavkaRepository.SaveChanges();

                return Ok(mapper.Map<StavkaPorudzbineConfirmation>(confirmation));
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
        //[Authorize(Policy = IdentityData.KorisnikPolicy)]
        public async Task<ActionResult<StavkaPorudzbineDTO>> UpdateStavka(StavkaPorudzbineUpdateDTO stavka)
        {
            try
            {
                //Proveriti da li postoji Stavka porudzbine koju zelimo da azuriramo
                var oldStavka = await stavkaRepository.GetStavkaById(stavka.StavkaID);

                if (oldStavka == null)
                {
                    return NotFound("Stavka porudzbine koju zelite azurirati ne postoji.");
                }

                StavkaPorudzbine stavkaEntity = mapper.Map<StavkaPorudzbine>(stavka);

                mapper.Map(stavkaEntity, oldStavka);

                await stavkaRepository.SaveChanges();
                return Ok(mapper.Map<StavkaPorudzbineDTO>(oldStavka));
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
        [HttpDelete("{stavkaId}")]
        //[Authorize(Policy = IdentityData.KorisnikPolicy)]
        public async Task<ActionResult> DeleteStavka(int stavkaId)
        {
            try
            {
                var stavka = await stavkaRepository.GetStavkaById(stavkaId);

                if (stavka == null)
                {
                    return NotFound("Stavka porudzbine sa datim ID-jem koju zelite obrisati ne postoji.");
                }

                await stavkaRepository.DeleteStavka(stavkaId);
                await stavkaRepository.SaveChanges();

                return Ok("Stavka porudzbine uspesno obrisana.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }
    }
}
