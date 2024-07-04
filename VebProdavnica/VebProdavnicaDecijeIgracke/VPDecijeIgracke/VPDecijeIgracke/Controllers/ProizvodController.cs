using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VPDecijeIgracke.Authentication;
using VPDecijeIgracke.Data;
using VPDecijeIgracke.Data.ProizvodData;
using VPDecijeIgracke.Data.Specifications;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/proizvod")]
    [Produces("application/json", "application/xml")]
    public class ProizvodController : Controller
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly IMapper mapper;

        /**/
        private readonly IGenericRepository<Proizvod> _proizRepo;
        private readonly IGenericRepository<Kategorija> _proizKatRepo;
        /**/
        public ProizvodController(IProizvodRepository proizvodRepository, IMapper mapper, IGenericRepository<Proizvod> proizRepo, IGenericRepository<Kategorija> proizKatRepo)
        {
            this.proizvodRepository = proizvodRepository;
            this.mapper = mapper;
            this._proizRepo = proizRepo;
            this._proizKatRepo = proizKatRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<ProizvodDTO>>> GetAllProizvodi(
           [FromQuery]ProizvodSpecParams proizvodParams)
        {
            var spec = new ProizvodWithKategorijeSpecification(proizvodParams);
            var countSpec = new ProizvodWithFiltersForCountSpecification(proizvodParams);
            var totalItems = await _proizRepo.CountAsync(countSpec);

            var proizvodi = await _proizRepo.ListAsync(spec);
            var data = mapper.Map<IReadOnlyList<Proizvod>, IReadOnlyList<ProizvodDTO>>(proizvodi);

            // Konvertuj IReadOnlyList<ProizvodDTO> u List<ProizvodDTO>
            var dataList = new List<ProizvodDTO>(data);

            if (proizvodi == null || proizvodi.Count == 0)
            {
                return NoContent();
            }

            return Ok(new Pagination<ProizvodDTO>(proizvodParams.PageIndex, proizvodParams.PageSize, 
                                                totalItems, dataList));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{proizvodId}")] 
        [EnableCors("AllowOrigin")]
        [AllowAnonymous]
        public async Task<ActionResult<ProizvodDTO>> GetProizvodById(int proizvodId)
        {
            var spec = new ProizvodWithKategorijeSpecification(proizvodId);

            var proizvod = await _proizRepo.GetEntityWithSpec(spec);

            if (proizvod == null)
            {
                return NotFound("Proizvod sa datim ID-jem nije pronadjen.");
            }

            return Ok(mapper.Map<ProizvodDTO>(proizvod));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult<ProizvodConfirmation>> CreateProizvod([FromBody] ProizvodCreationDTO proizvod)
        {
            try
            {
                Proizvod proizvod1 = mapper.Map<Proizvod>(proizvod);

                var (isValid, errorMessage) = await ValidateProizvod(proizvod1);

                if (!isValid)
                {
                    return BadRequest(errorMessage);
                }

                ProizvodConfirmation confirmation = await proizvodRepository.CreateProizvod(proizvod1);
                await proizvodRepository.SaveChanges();

                return Ok(mapper.Map<ProizvodConfirmation>(confirmation));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error:" + ex);
            }
        }

        private async Task<(bool, string)> ValidateProizvod(Proizvod proizvod)
        {
            List<Proizvod> proizvodi = await proizvodRepository.GetAllProizvodi();

            bool nazivProizvodaPostoji = proizvodi.Any(p => p.NazivProizvoda == proizvod.NazivProizvoda);
            bool slikaURLPostoji = proizvodi.Any(p => p.SlikaURL == proizvod.SlikaURL);

            if (nazivProizvodaPostoji)
            {
                return (false, "Naziv proizvoda vec postoji.");
            }

            if (slikaURLPostoji)
            {
                return (false, "Dati URL slike vec postoji.");
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
        public async Task<ActionResult<ProizvodDTO>> UpdateProizvod(ProizvodUpdateDTO proizvod)
        {
            try
            {
                //Proveriti da li postoji Proizvod koji zelimo da azuriramo
                var oldProizvod = await proizvodRepository.GetProizvodById(proizvod.ProizvodID);

                if (oldProizvod == null)
                {
                    return NotFound("Proizvod koji zelite azurirati ne postoji.");
                }

                Proizvod proizvodEntity = mapper.Map<Proizvod>(proizvod);

                mapper.Map(proizvodEntity, oldProizvod);

                await proizvodRepository.SaveChanges();
                return Ok(mapper.Map<ProizvodDTO>(oldProizvod));
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
        [HttpDelete("{proizvodId}")]
        [Authorize(Policy = IdentityData.AdministratorPolicy)]
        public async Task<ActionResult> DeleteProizvod(int proizvodId)
        {
            try
            {
                var proizvod = await proizvodRepository.GetProizvodById(proizvodId);

                if (proizvod == null)
                {
                    return NotFound("Proizvod sa datim ID-jem koji zelite obrisati ne postoji.");
                }

                await proizvodRepository.DeleteProizvod(proizvodId);
                await proizvodRepository.SaveChanges();

                return Ok("Proizvod uspesno obrisan.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error:" + ex);

            }
        }

        //pretraga proizvoda po nazivu
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<ProizvodDTO>>> SearchProizvodiByNaziv(string search)
        {
            var proizvodi = await proizvodRepository.SearchProizvodiByNaziv(search);

            if (proizvodi == null || proizvodi.Count == 0)
            {
                return NotFound("Ne postoji nijedan proizvod koji odgovara vasoj pretrazi.");
            }

            return Ok(proizvodi);
        }

        //pretraga proizvoda u odredjenom opsegu cene
        [HttpGet("cenaProizvoda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ProizvodDTO>>> GetProizvodiByCenaProizvoda(decimal? minCena, decimal? maxCena)
        {
            var proizvodi = await proizvodRepository.GetProizvodiByCenaProizvoda(minCena, maxCena);

            if (proizvodi == null || proizvodi.Count == 0)
            {
                return NotFound("Nije pronadjen nijedan proizvod u ovom opsegu cena.");
            }

            return Ok(proizvodi);
        }

        /**/
        [HttpGet("kategorija")]
        public async Task<ActionResult<IReadOnlyList<Kategorija>>> GetKategorije()
        {
            return Ok(await _proizKatRepo.ListAllAsync());
        }
        /**/
    }
}
