using AutoMapper;
using Travel.Models.Travel;
using Travel.ViewModel;

namespace Travel.AutoMapperProfile
{
    public class TravelPolicyProfile : Profile
    {
        public TravelPolicyProfile()
        {
            CreateMap<TravelVM, TravelBn>()
                .ForMember(
                    dest => dest.PersonName,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.PersonName}")
                )
                .ForMember(
                    dest => dest.PersonLname,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.PersonLname}")
                )



                .ForMember(
                    dest => dest.PersonJens,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.PersonJensId}")
                )
                //.ForMember(
                //    dest => dest.PersonKind,
                //    opt => opt.MapFrom(src => $"{src.BimeGozar.PersonKindId}")
                //)

                .ForMember(
                    dest => dest.Mobile,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.Mobile}")
                )
                .ForMember(
                    dest => dest.Tel,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.Tel}")
                ).ForMember(
                    dest => dest.PersonAddress,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.PersonAddress}")
                )
                .ForMember(
                    dest => dest.CodePosti,
                    opt => opt.MapFrom(src => $"{src.BimeGozar.CodePosti}")
                )

            .ForMember(
                dest => dest.FatherName,
                opt => opt.MapFrom(src => $"{src.BimeGozar.FatherName}")
            ).ForMember(
                dest => dest.PassportNo,
                opt => opt.MapFrom(src => $"{src.BimeGozar.PassportNo}")
            );
            //.ForMember(
            //    dest => dest.Seri,
            //    opt => opt.MapFrom(src => $"{src.BimeGozar.Seri}")
            //).ForMember(
            //    dest => dest.Serial,
            //    opt => opt.MapFrom(src => $"{src.BimeGozar.Serial}")
            //)
            //.ForMember(
            //    dest => dest.Gid,
            //    opt => opt.MapFrom(src => $"{src.Invoice.Gid}")
            //)
            //.ForMember(
            //    dest => dest.GrpTakhfif,
            //    opt => opt.MapFrom(src => $"{src.Invoice.GrpTakhfifId}")
           // )
            
            
            //.ForMember(
            //    dest => dest.CoverLimit,
            //    opt => opt.MapFrom(src => $"{src.Invoice.CoverLimit}")
            //).ForMember(
            //    dest => dest.SafarKind,
            //    opt => opt.MapFrom(src => $"{src.Invoice.SafarKindId}")
            //)
            //.ForMember(
            //    dest => dest.ModdatKind,
            //    opt => opt.MapFrom(src => $"{src.Invoice.ModdatKindId}")
            //)
            
            
            //.ForMember(
            //    dest => dest.Moddat,
            //    opt => opt.MapFrom(src => $"{src.Invoice.Moddat}")
            //).ForMember(
            //    dest => dest.Country,
            //    opt => opt.MapFrom(src => $"{src.Invoice.CountryId}")
            //).ForMember(
            //    dest => dest.CountryOut,
            //    opt => opt.MapFrom(src => $"{src.Invoice.CountryOutId}")
            //)




            //.ForMember(
            //    dest => dest.LocationZoneId,
            //    opt => opt.MapFrom(src => $"{src.Invoice.LocationZoneId}")
            //)

            //.ForMember(
            //    dest => dest.CodeSodur,
            //    opt => opt.MapFrom(src => $"{src.Invoice.SodurId}")
            //).ForMember(
            //    dest => dest.AgentCode,
            //    opt => opt.MapFrom(src => $"{src.Invoice.AgentId}")
            //);
            
            //.ForMember(
            //    dest => dest.Bimekind,
            //    opt => opt.MapFrom(src => $"{src.Invoice.BimekindId}")
            //)

        }
    }
}
