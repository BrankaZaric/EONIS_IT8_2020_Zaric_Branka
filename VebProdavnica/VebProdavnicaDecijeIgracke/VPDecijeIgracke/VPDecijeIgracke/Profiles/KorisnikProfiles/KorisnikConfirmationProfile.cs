using AutoMapper;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Profiles.KorisnikProfiles
{
    public class KorisnikConfirmationProfile : Profile
    {
        public KorisnikConfirmationProfile()
        {
            CreateMap<Korisnik, KorisnikConfirmation>()
                .ForMember(dest => dest.KorisnikID, opt => opt.MapFrom(src =>
                src.KorisnikID))
                .ForMember(dest => dest.Ime, opt => opt.MapFrom(src =>
                src.Ime))
                .ForMember(dest => dest.Prezime, opt => opt.MapFrom(src =>
                src.Prezime))
                .ForMember(dest => dest.KorisnickoIme, opt => opt.MapFrom(src =>
                src.KorisnickoIme))
                .ForMember(dest => dest.Lozinka, opt => opt.MapFrom(src =>
                src.Lozinka))
                .ForMember(dest => dest.Adresa, opt => opt.MapFrom(src =>
                src.Adresa))
                .ForMember(dest => dest.Telefon, opt => opt.MapFrom(src =>
                src.Telefon));


        }
    }
}
