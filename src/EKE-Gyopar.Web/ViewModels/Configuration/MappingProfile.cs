using AutoMapper;
using EKE.Data.Entities.Gyopar;
using System.Collections.Generic;

namespace EKE_Gyopar.Web.ViewModels.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleSerchItemVM>()
                .ForMember(dest => dest.AuthorName, opt=>
                {
                    opt.Condition(src => src.Author != null);
                    opt.MapFrom(src => src.Author.Name);
                
                })
               .ForMember(dest => dest.PublishYear, opt =>
                {
                    opt.Condition(src => src.Magazine != null);
                    opt.MapFrom(src => src.Magazine.PublishYear);

                })
                .ForMember(dest => dest.PublishSection, opt =>
                {
                    opt.Condition(src => src.Magazine != null);
                    opt.MapFrom(src => src.Magazine.PublishSection);

                });
        }
    }
}
