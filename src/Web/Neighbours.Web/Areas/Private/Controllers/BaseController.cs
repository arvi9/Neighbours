namespace Neighbours.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Ninject;
    using Services.Common.Contracts;

    public class BaseController : Controller
    {
        [Inject]
        public ICacheService Cache { get; set; }

        [Inject]
        public IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}