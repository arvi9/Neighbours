namespace Neighbours.Web.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Common.Constants;

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
    }
}