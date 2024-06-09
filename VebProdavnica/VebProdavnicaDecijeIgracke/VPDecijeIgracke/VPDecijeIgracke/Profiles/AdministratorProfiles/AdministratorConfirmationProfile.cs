using AutoMapper;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Profiles.AdministratorProfiles
{
    public class AdministratorConfirmationProfile : Profile
    {
        public AdministratorConfirmationProfile() 
        {
            CreateMap<Administrator, AdministratorConfirmation>()
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID))
                .ForMember(dest => dest.ImeAdmin, opt => opt.MapFrom(src =>
                src.ImeAdmin))
                .ForMember(dest => dest.PrezimeAdmin, opt => opt.MapFrom(src =>
                src.PrezimeAdmin))
                .ForMember(dest => dest.KorisnickoImeAdmin, opt => opt.MapFrom(src =>
                src.KorisnickoImeAdmin))
                .ForMember(dest => dest.LozinkaAdmin, opt => opt.MapFrom(src =>
                src.LozinkaAdmin))
                .ForMember(dest => dest.AdresaAdmin, opt => opt.MapFrom(src =>
                src.AdresaAdmin))
                .ForMember(dest => dest.TelefonAdmin, opt => opt.MapFrom(src =>
                src.TelefonAdmin));
        }
    }
}
