using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.Specifications
{
    public class ProizvodWithKategorijeSpecification : Specification<Proizvod>
    {
        public ProizvodWithKategorijeSpecification(ProizvodSpecParams proizvodParams) 
            : base(x => 
                (string.IsNullOrEmpty(proizvodParams.Search) || x.NazivProizvoda.ToLower().Contains
                (proizvodParams.Search)) &&
                (!proizvodParams.KategorijaId.HasValue || x.KategorijaID == proizvodParams.KategorijaId)
            )
        {
            AddInclude(k => k.Kategorija);
            AddOrderBy(x => x.NazivProizvoda);
            ApplyPaging(proizvodParams.PageSize * (proizvodParams.PageIndex - 1),
                        proizvodParams.PageSize);

            if(!string.IsNullOrEmpty(proizvodParams.Sort))
            {
                switch(proizvodParams.Sort)
                {
                    case "cenaAsc":
                        AddOrderBy(c => c.Cena);
                        break;
                    case "cenaDesc":
                        AddOrderByDescending(c => c.Cena);
                        break;
                    default:
                        AddOrderBy(x => x.NazivProizvoda);
                        break;
                }
            }
        }

        public ProizvodWithKategorijeSpecification(int id) : base(x => x.ProizvodID == id)
        {
            AddInclude(k => k.Kategorija);
        }
    }
}
