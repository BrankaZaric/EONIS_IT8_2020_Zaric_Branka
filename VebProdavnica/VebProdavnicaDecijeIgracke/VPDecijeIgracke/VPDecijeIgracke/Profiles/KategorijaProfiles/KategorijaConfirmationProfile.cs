using AutoMapper;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Profiles.KategorijaProfiles
{
    public class KategorijaConfirmationProfile : Profile
    {
        public KategorijaConfirmationProfile() 
        {
            CreateMap<Kategorija, KategorijaConfirmation>()
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.NazivKategorije, opt => opt.MapFrom(src =>
                src.NazivKategorije));
        }
    }
}
