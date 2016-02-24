namespace Neighbours.Web.Areas.Administrator.Controllers
{
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models.Communities;
    using Kendo.Mvc.Extensions;
    using Data.Models;

    public class CommunitiesController : Controller
    {
        private IUsersService users;
        private ICommunitiesService communities;

        public CommunitiesController(IUsersService users, ICommunitiesService communities)
        {
            this.users = users;
            this.communities = communities;
        }

        // GET: Administrator/Communities
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Administrator/Communities
        public ActionResult Details(int id)
        {
            return this.View();
        }

        public ActionResult CommunitiesRead([DataSourceRequest]DataSourceRequest request)
        {
            var userId = this.User.Identity.GetUserId();

            var model = this.communities.GetAllMine(userId).To<CommunityViewModel>();

            DataSourceResult result = model.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommunitiesUpdate([DataSourceRequest]DataSourceRequest request, CommunityViewModel model, HttpPostedFileBase file)
        {
            if (this.ModelState.IsValid)
            {
                this.communities.UpdateName(model.Id, model.Name);
            }

            var communities = this.communities.GetAll().To<CommunityViewModel>().ToDataSourceResult(request);

            return this.Json(communities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommunitiesDestroy([DataSourceRequest]DataSourceRequest request, CommunityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.communities.Delete(model.Id);
            }

            var communities = this.communities.GetAll().To<CommunityViewModel>().ToDataSourceResult(request);

            return this.Json(communities, JsonRequestBehavior.AllowGet);
        }
    }
}