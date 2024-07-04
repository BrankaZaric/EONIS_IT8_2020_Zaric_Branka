using AutoMapper;
using VPDecijeIgracke.Models.AdministratorModel;

namespace VPDecijeIgracke.Profiles.AdministratorProfiles
{
    public class AdministratorProfile : Profile
    {
        public AdministratorProfile() 
        {
            CreateMap<Administrator, AdministratorDTO>()
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID))
                .ForMember(dest => dest.ImeAdmin, opt => opt.MapFrom(src =>
                src.ImeAdmin))
                .ForMember(dest => dest.PrezimeAdmin, opt => opt.MapFrom(src =>
                src.PrezimeAdmin))
                .ForMember(dest => dest.KorisnickoImeAdmin, opt => opt.MapFrom(src =>
                src.KorisnickoImeAdmin))
                .ForMember(dest => dest.AdresaAdmin, opt => opt.MapFrom(src =>
                src.AdresaAdmin))
                .ForMember(dest => dest.TelefonAdmin, opt => opt.MapFrom(src =>
                src.TelefonAdmin));

            CreateMap<Administrator, AdministratorLozinkaDTO>()
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

            CreateMap<AdministratorCreationDTO, Administrator>()
                .ForMember(dest => dest.ImeAdmin, opt => opt.MapFrom(src =>
                src.ImeAdmin))
                .ForMember(dest => dest.PrezimeAdmin, opt => opt.MapFrom(src =>
                src.PrezimeAdmin))
                .ForMember(dest => dest.KorisnickoImeAdmin, opt => opt.MapFrom(src =>
                src.KorisnickoImeAdmin))
                .ForMember(dest => dest.AdresaAdmin, opt => opt.MapFrom(src =>
                src.AdresaAdmin))
                .ForMember(dest => dest.TelefonAdmin, opt => opt.MapFrom(src =>
                src.TelefonAdmin));

            CreateMap<AdministratorUpdateDTO, Administrator>()
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID))
                .ForMember(dest => dest.ImeAdmin, opt => opt.MapFrom(src =>
                src.ImeAdmin))
                .ForMember(dest => dest.PrezimeAdmin, opt => opt.MapFrom(src =>
                src.PrezimeAdmin))
                .ForMember(dest => dest.KorisnickoImeAdmin, opt => opt.MapFrom(src =>
                src.KorisnickoImeAdmin))
                .ForMember(dest => dest.AdresaAdmin, opt => opt.MapFrom(src =>
                src.AdresaAdmin))
                .ForMember(dest => dest.TelefonAdmin, opt => opt.MapFrom(src =>
                src.TelefonAdmin));

            CreateMap<Administrator, Administrator>();
        }
    }
}
