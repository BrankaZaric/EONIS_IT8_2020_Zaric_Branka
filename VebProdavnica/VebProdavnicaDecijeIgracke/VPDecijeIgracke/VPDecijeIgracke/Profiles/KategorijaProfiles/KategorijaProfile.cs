using AutoMapper;
using VPDecijeIgracke.Models.KategorijaProizvodaModel;

namespace VPDecijeIgracke.Profiles.KategorijaProfiles
{
    public class KategorijaProfile : Profile
    {
        public KategorijaProfile() 
        {
            CreateMap<Kategorija, KategorijaDTO>()
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.NazivKategorije, opt => opt.MapFrom(src =>
                src.NazivKategorije));

            CreateMap<KategorijaCreationDTO, Kategorija>()
                .ForMember(dest => dest.NazivKategorije, opt => opt.MapFrom(src =>
                src.NazivKategorije));

            CreateMap<KategorijaUpdateDTO, Kategorija>()
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.NazivKategorije, opt => opt.MapFrom(src =>
                src.NazivKategorije));

            CreateMap<Kategorija, Kategorija>();
        }
    }
}
