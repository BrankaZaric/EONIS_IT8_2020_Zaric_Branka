using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data.KategorijaProizvodaData;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/kategorija")]
    [Produces("application/json", "application/xml")]
    public class KategorijaController : Controller
    {
        private readonly IKategorijaRepository kategorijaRepository;
        private readonly IMapper mapper;

        public KategorijaController(IKategorijaRepository kategorijaRepository, IMapper mapper)
        {
            this.kategorijaRepository = kategorijaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<KategorijaDTO>>> GetAllKategorije()
        {
            var kategorije = await kategorijaRepository.GetAllKategorije();

            if (kategorije == null || kategorije.Count == 0)
            {
                return NoContent();
            }

            return Ok(kategorije);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{kategorijaId}")] 
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        public async Task<ActionResult<KategorijaDTO>> GetKategorijaById(int kategorijaId)
        {
            var kategorija = await kategorijaRepository.GetKategorijaById(kategorijaId);

            if (kategorija == null)
            {
                return NotFound("Kategorija sa datim ID-jem nije pronadjena.");
            }

            return Ok(mapper.Map<KategorijaDTO>(kategorija));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<KategorijaConfirmation>> CreateKategorija([FromBody] KategorijaCreationDTO kategorija)
        {
            try
            {
                Kategorija kategorija1 = mapper.Map<Kategorija>(kategorija);

                var (isValid, errorMessage) = await ValidateKategorija(kategorija1);

                if (!isValid)
                {
                    return BadRequest(errorMessage);
                }

                KategorijaConfirmation confirmation = await kategorijaRepository.CreateKategorija(kategorija1);
                await kategorijaRepository.SaveChanges();

                return Ok(mapper.Map<KategorijaConfirmation>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error:" + ex);
            }
        }

        private async Task<(bool, string)> ValidateKategorija(Kategorija kategorija)
        {
            List<Kategorija> kategorije = await kategorijaRepository.GetAllKategorije();

            bool nazivKategorijePostoji = kategorije.Any(k => k.NazivKategorije == kategorija.NazivKategorije);

            if (nazivKategorijePostoji)
            {
                return (false, "Naziv kategorije vec postoji.");
            }

            return (true, "Validacija uspesna.");
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<KategorijaDTO>> UpdateKategorija(KategorijaUpdateDTO kategorija)
        {
            try
            {
                //Proveriti da li postoji Kategorija koju zelimo da azuriramo
                var oldKategorija = await kategorijaRepository.GetKategorijaById(kategorija.KategorijaID);

                if (oldKategorija == null)
                {
                    return NotFound("Kategorija koju zelite azurirati ne postoji.");
                }

                Kategorija kategorijaEntity = mapper.Map<Kategorija>(kategorija);

                mapper.Map(kategorijaEntity, oldKategorija);

                await kategorijaRepository.SaveChanges();
                return Ok(mapper.Map<KategorijaDTO>(oldKategorija));
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
        [HttpDelete("{kategorijaId}")]
        //[Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult> DeleteKategorija(int kategorijaId)
        {
            try
            {
                var kategorija = await kategorijaRepository.GetKategorijaById(kategorijaId);

                if (kategorija == null)
                {
                    return NotFound("Kategorija sa datim ID-jem koju zelite obrisati ne postoji.");
                }

                await kategorijaRepository.DeleteKategorija(kategorijaId);
                await kategorijaRepository.SaveChanges();

                return Ok("Kategorija uspesno obrisana.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }
    }
}
