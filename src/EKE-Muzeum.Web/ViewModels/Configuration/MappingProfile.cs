using AutoMapper;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Museum;
using System.Collections.Generic;

namespace EKE_Muzeum.Web.ViewModels.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Element, HomeVM>();

            CreateMap<List<Element>, HomeVM>()
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src));
        }
    }
}
