[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Neighbours.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Neighbours.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Neighbours.Web.App_Start
{
    using AutoMapper;
    using Common.Constants;
    using Data;
    using Data.Common.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Services.Common.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<DbContext>().To<NeighboursDbContext>().InRequestScope();


            //kernel.Bind<IUserServices>().To<UserServices>();
            // TODO: Check if exception for IService
            kernel.Bind(k => k
                .From(
                    Assemblies.ServicesData)
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface());

            var types = AutoMapperConfig.GetTypesInAssembly();
            var config = AutoMapperConfig.ConfigureAutomapper(types);

            var mapper = config.CreateMapper();

            kernel.Bind<MapperConfiguration>().ToMethod(c => config).InSingletonScope();
            kernel.Bind<IMapper>().ToConstant(mapper);
        }
    }
}
