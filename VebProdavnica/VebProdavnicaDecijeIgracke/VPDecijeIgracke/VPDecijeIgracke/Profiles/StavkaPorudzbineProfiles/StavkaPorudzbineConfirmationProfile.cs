using AutoMapper;
using VPDecijeIgracke.Models.StavkaPorudzbineModel;

namespace VPDecijeIgracke.Profiles.StavkaPorudzbineProfiles
{
    public class StavkaPorudzbineConfirmationProfile : Profile
    {
        public StavkaPorudzbineConfirmationProfile() 
        {
            CreateMap<StavkaPorudzbine, StavkaPorudzbineConfirmation>()
                .ForMember(dest => dest.StavkaID, opt => opt.MapFrom(src =>
                src.StavkaID))
                .ForMember(dest => dest.CenaStavka, opt => opt.MapFrom(src =>
                src.CenaStavka))
                .ForMember(dest => dest.KolicinaStavka, opt => opt.MapFrom(src =>
                src.KolicinaStavka))
                .ForMember(dest => dest.ProizvodID, opt => opt.MapFrom(src =>
                src.ProizvodID))
                .ForMember(dest => dest.Proizvod, opt => opt.MapFrom(src =>
                src.Proizvod))
                .ForMember(dest => dest.PorudzbinaID, opt => opt.MapFrom(src =>
                src.PorudzbinaID))
                .ForMember(dest => dest.Porudzbina, opt => opt.MapFrom(src =>
                src.Porudzbina));
        }
    }
}
