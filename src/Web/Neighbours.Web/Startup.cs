using Microsoft.Owin;
using Neighbours.Web.App_Start;
using Neighbours.Web.Common.Constants;
using Owin;

[assembly: OwinStartupAttribute(typeof(Neighbours.Web.Startup))]
namespace Neighbours.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// Static API before 4.2.0. Use only with the old Automapper
            // AutoMapperConfig.RegisterMappings(Assemblies.ViewModels);
            this.ConfigureAuth(app);
        }
    }
}
