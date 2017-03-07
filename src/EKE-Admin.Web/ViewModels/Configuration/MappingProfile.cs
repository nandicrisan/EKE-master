using AutoMapper;
using EKE.Data.Entities.Gyopar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKE_Admin.Web.ViewModels.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Magazine, MagazineListVM>();
            CreateMap<MagazineCategory, MagazineListVM>();

            CreateMap<List<Magazine>, MagazineListVM>()
                .ForMember(dest => dest.Magazines, opt => opt.MapFrom(src => src));
            CreateMap<List<MagazineCategory>, MagazineListVM>()
                .ForMember(dest => dest.MagazineCategories, opt => opt.MapFrom(src => src));
        }
    }
}
