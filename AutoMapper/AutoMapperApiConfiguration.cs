using AutoMapper;

namespace Nop.Plugin.Api.AutoMapper
{
    public static class AutoMapperApiConfiguration
    {
        private static IMapper s_mapper;
        private static readonly object s_mapperLockObject = new object();

        public static IMapper Mapper
        {
            get
            {
                if (s_mapper == null)
                {
                    lock (s_mapperLockObject)
                    {
                        if (s_mapper == null)
                        {
                            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new ApiMapperConfiguration()));

                            s_mapper = mapperConfiguration.CreateMapper();
                        }
                    }
                }

                return s_mapper;
            }
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
