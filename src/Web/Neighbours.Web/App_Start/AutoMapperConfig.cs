namespace Neighbours.Web.App_Start
{
    using AutoMapper;
    using Infrastructure.Mapping;
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    public static class AutoMapperConfig
    {
        public static IList<Type> GetTypesInAssembly()
        {
            var assemblies = new string[] { Assemblies.ViewModels };

            var types = new List<Type>();
            foreach (var assembly in assemblies.Select(Assembly.Load))
            {
                types.AddRange(assembly.GetExportedTypes());
            }

            return types;
        }

        public static MapperConfiguration ConfigureAutomapper(IList<Type> types)
        {
            var profiles =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(Profile).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (Profile)Activator.CreateInstance(type.t));


            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return config;
        }

        // Static API before 4.2.0
        //public static void RegisterMappings(params string[] assemblies)
        //{
        //    //var config = new MapperConfiguration(cfg => cfg.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t)));

        //    //Static API Before version 4.2.0

        //    Mapper.Configuration.ConstructServicesUsing(t => DependencyResolver.Current.GetService(t));

        //    var types = new List<Type>();
        //    foreach (var assembly in assemblies.Select(Assembly.Load))
        //    {
        //        types.AddRange(assembly.GetExportedTypes());
        //    }

        //    LoadStandardMappings(types);
        //    LoadCustomMappings(types);

        //    // Showcase system specific code
        //    //ExplicitMaps.AddMaps(Mapper.Configuration);
        //}

        //private static void LoadStandardMappings(IEnumerable<Type> types)
        //{
        //    var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
        //        .Where(
        //            type =>
        //                type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
        //                !type.t.IsAbstract
        //                && !type.t.IsInterface)
        //        .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });


        //    //var config = new MapperConfiguration(cfg =>
        //    //{
        //    //    foreach (var map in maps)
        //    //    {
        //    //        cfg.CreateMap(map.Source, map.Destination);
        //    //        cfg.CreateMap(map.Destination, map.Source);
        //    //    }
        //    //});

        //    //Static API Before version 4.2.0
        //    foreach (var map in maps)
        //    {
        //        Mapper.CreateMap(map.Source, map.Destination);
        //        Mapper.CreateMap(map.Destination, map.Source);
        //    }
        //}

        //private static void LoadCustomMappings(IEnumerable<Type> types)
        //{
        //    var maps =
        //        types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
        //            .Where(
        //                type =>
        //                    typeof(IHaveCustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
        //                    !type.t.IsInterface)
        //            .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

        //    foreach (var map in maps)
        //    {
        //        //map.CreateMappings();

        //        //Static API Before version 4.2.0
        //        map.CreateMappings(Mapper.Configuration);
        //    }

        //}
    }
}