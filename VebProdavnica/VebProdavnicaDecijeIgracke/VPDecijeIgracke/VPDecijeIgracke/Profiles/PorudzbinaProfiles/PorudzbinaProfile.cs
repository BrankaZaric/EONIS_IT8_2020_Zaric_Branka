using AutoMapper;
using VPDecijeIgracke.Models.PorudzbinaModel;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Profiles.PorudzbinaProfiles
{
    public class PorudzbinaProfile : Profile
    {
        public PorudzbinaProfile() 
        {

            CreateMap<Porudzbina, PorudzbinaDTO>()
                .ForMember(dest => dest.PorudzbinaID, opt => opt.MapFrom(src =>
                src.PorudzbinaID))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src =>
                src.Datum))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Status))
                .ForMember(dest => dest.Iznos, opt => opt.MapFrom(src =>
                src.Iznos))
                .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src =>
                src.PaymentIntentId))
                .ForMember(dest => dest.ClientSecret, opt => opt.MapFrom(src =>
                src.ClientSecret))
                .ForMember(dest => dest.KorisnikID, opt => opt.MapFrom(src =>
                src.KorisnikID))
                .ForMember(dest => dest.Korisnik, opt => opt.MapFrom(src =>
                src.Korisnik));

            CreateMap<PorudzbinaCreationDTO, Porudzbina>()
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src =>
                src.Datum))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Status))
                .ForMember(dest => dest.Iznos, opt => opt.MapFrom(src =>
                src.Iznos))
                .ForMember(dest => dest.KorisnikID, opt => opt.MapFrom(src =>
                src.KorisnikID));

            CreateMap<PorudzbinaUpdateDTO, Porudzbina>()
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

            CreateMap<Porudzbina, Porudzbina>();

            CreateMap<Porudzbina, PorudzbinaPaymentDTO>()
                .ForMember(dest => dest.PorudzbinaID, opt => opt.MapFrom(src =>
                src.PorudzbinaID))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src =>
                src.Datum))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.Status))
                .ForMember(dest => dest.Iznos, opt => opt.MapFrom(src =>
                src.Iznos))
                .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src =>
                src.PaymentIntentId))
                .ForMember(dest => dest.ClientSecret, opt => opt.MapFrom(src =>
                src.ClientSecret))
                .ForMember(dest => dest.KorisnikID, opt => opt.MapFrom(src =>
                src.KorisnikID));
        }
    }
}
