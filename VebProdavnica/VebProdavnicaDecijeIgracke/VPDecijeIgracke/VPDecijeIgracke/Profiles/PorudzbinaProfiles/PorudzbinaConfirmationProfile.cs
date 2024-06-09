using AutoMapper;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Profiles.PorudzbinaProfiles
{
    public class PorudzbinaConfirmationProfile : Profile
    {
        public PorudzbinaConfirmationProfile() 
        {
            CreateMap<Porudzbina, PorudzbinaConfirmation>()
                .ForMember(dest => dest.PorudzbinaID, opt => opt.MapFrom(src =>
                src.PorudzbinaID))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src =>
                src.Datum))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Status))
                .ForMember(dest => dest.Iznos, opt => opt.MapFrom(src =>
                src.Iznos))
                .ForMember(dest => dest.KorisnikID, opt => opt.MapFrom(src =>
                src.KorisnikID))
                .ForMember(dest => dest.Korisnik, opt => opt.MapFrom(src =>
                src.Korisnik));
        }
    }
}
