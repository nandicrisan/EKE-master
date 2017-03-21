using AutoMapper;
using EKE.Data.Entities;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Identity.AccountViewModels;
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
            CreateMap<Article, MagazineListVM>();

            CreateMap<Magazine, MagazineVM>();
            CreateMap<MagazineCategory, MagazineVM>();
            CreateMap<Article, MagazineVM>();

            CreateMap<List<Magazine>, MagazineListVM>()
                .ForMember(dest => dest.Magazines, opt => opt.MapFrom(src => src));
            CreateMap<List<MagazineCategory>, MagazineListVM>()
                .ForMember(dest => dest.MagazineCategories, opt => opt.MapFrom(src => src));

            CreateMap<List<Article>, MagazineVM>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src));
            CreateMap<List<MagazineCategory>, MagazineVM>()
                .ForMember(dest => dest.MagazineCategories, opt => opt.MapFrom(src => src));

            CreateMap<List<ApplicationUser>, UserManagementVM>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src));
            CreateMap<RegisterViewModel, UserManagementVM>();
            CreateMap<List<ApplicationUser>, UserManagementVM>()
               .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src));
        }
    }
}
