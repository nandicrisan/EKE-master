using AutoMapper;
using EKE.Data.Entities;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Identity.AccountViewModels;
using EKE.Service.ServiceModel;
using System.Collections.Generic;

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

            CreateMap<MuseumVM, MuseumSM>();

            CreateMap<List<Magazine>, MagazineListVM>()
                .ForMember(dest => dest.Magazines, opt => opt.MapFrom(src => src));
            CreateMap<List<MagazineCategory>, MagazineListVM>()
                .ForMember(dest => dest.MagazineCategories, opt => opt.MapFrom(src => src));
            CreateMap<List<Tag>, MagazineListVM>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src));

            CreateMap<List<Article>, MagazineVM>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src));
            CreateMap<List<MagazineCategory>, MagazineVM>()
                .ForMember(dest => dest.MagazineCategories, opt => opt.MapFrom(src => src));
            CreateMap<List<Tag>, MagazineVM>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src));
            CreateMap<List<Magazine>, MagazineVM>()
                .ForMember(dest => dest.Magazines, opt => opt.MapFrom(src => src));

            CreateMap<List<ApplicationUser>, UserManagementVM>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src));
            CreateMap<RegisterViewModel, UserManagementVM>();
            CreateMap<List<ApplicationRole>, UserManagementVM>()
               .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src));

            CreateMap<Article, ArticleVM>()
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src));
            CreateMap<List<Tag>, ArticleVM>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src));
            CreateMap<List<Author>, ArticleVM>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src));

            CreateMap<XEditVM, XEditSM>()
                .ForMember(dest => dest.PrimaryKey, opt => opt.MapFrom(src => src.pk))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));
        }
    }
}
