using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.Specifications
{
    public class ProizvodWithFiltersForCountSpecification : Specification<Proizvod>
    {
        public ProizvodWithFiltersForCountSpecification(ProizvodSpecParams proizvodParams) 
            : base (x =>
                (string.IsNullOrEmpty(proizvodParams.Search) || x.NazivProizvoda.ToLower().Contains
                (proizvodParams.Search)) &&
                (!proizvodParams.KategorijaId.HasValue || x.KategorijaID == proizvodParams.KategorijaId))
        { 
        
        }
    }
}
