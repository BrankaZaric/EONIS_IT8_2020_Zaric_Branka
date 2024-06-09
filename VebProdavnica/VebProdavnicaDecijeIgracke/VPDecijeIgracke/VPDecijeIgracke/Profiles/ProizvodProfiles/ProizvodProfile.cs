using AutoMapper;
using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Profiles.ProizvodProfiles
{
    public class ProizvodProfile : Profile
    {
        public ProizvodProfile() 
        {
            CreateMap<Proizvod, ProizvodDTO>()
                .ForMember(dest => dest.ProizvodID, opt => opt.MapFrom(src =>
                src.ProizvodID))
                .ForMember(dest => dest.NazivProizvoda, opt => opt.MapFrom(src =>
                src.NazivProizvoda))
                .ForMember(dest => dest.Opis, opt => opt.MapFrom(src =>
                src.Opis))
                .ForMember(dest => dest.Cena, opt => opt.MapFrom(src =>
                src.Cena))
                .ForMember(dest => dest.Kolicina, opt => opt.MapFrom(src =>
                src.Kolicina))
                .ForMember(dest => dest.SlikaURL, opt => opt.MapFrom(src =>
                src.SlikaURL))
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.Kategorija, opt => opt.MapFrom(src =>
                src.Kategorija))
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID))
                .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src =>
                src.Administrator));

            CreateMap<ProizvodCreationDTO, Proizvod>()
                .ForMember(dest => dest.NazivProizvoda, opt => opt.MapFrom(src =>
                src.NazivProizvoda))
                .ForMember(dest => dest.Opis, opt => opt.MapFrom(src =>
                src.Opis))
                .ForMember(dest => dest.Cena, opt => opt.MapFrom(src =>
                src.Cena))
                .ForMember(dest => dest.Kolicina, opt => opt.MapFrom(src =>
                src.Kolicina))
                .ForMember(dest => dest.SlikaURL, opt => opt.MapFrom(src =>
                src.SlikaURL))
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID));

            CreateMap<ProizvodUpdateDTO, Proizvod>()
                .ForMember(dest => dest.ProizvodID, opt => opt.MapFrom(src =>
                src.ProizvodID))
                .ForMember(dest => dest.NazivProizvoda, opt => opt.MapFrom(src =>
                src.NazivProizvoda))
                .ForMember(dest => dest.Opis, opt => opt.MapFrom(src =>
                src.Opis))
                .ForMember(dest => dest.Cena, opt => opt.MapFrom(src =>
                src.Cena))
                .ForMember(dest => dest.Kolicina, opt => opt.MapFrom(src =>
                src.Kolicina))
                .ForMember(dest => dest.SlikaURL, opt => opt.MapFrom(src =>
                src.SlikaURL))
                .ForMember(dest => dest.KategorijaID, opt => opt.MapFrom(src =>
                src.KategorijaID))
                .ForMember(dest => dest.AdminID, opt => opt.MapFrom(src =>
                src.AdminID))
                .ForMember(dest => dest.Kategorija, opt => opt.MapFrom(src =>
                src.Kategorija))
                .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src =>
                src.Administrator));

            CreateMap<Proizvod, Proizvod>();
        }
    }
}
