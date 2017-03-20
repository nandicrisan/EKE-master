using AutoMapper;

namespace EKE_Gyopar.Web.ViewModels.Configuration
{
    public static class TDestination
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }
    }
}
