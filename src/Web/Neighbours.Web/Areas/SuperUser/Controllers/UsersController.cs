namespace Neighbours.Web.Areas.SuperUser.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models.Users;
    using Services.Data.Contracts;

    public class UsersController : BaseController
    {
        private IUsersService users;
        private ICommunitiesService communities;

        public UsersController(IUsersService users, ICommunitiesService communities)
        {
            this.users = users;
            this.communities = communities;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult UsersRead([DataSourceRequest]DataSourceRequest request)
        {
            var model = this.users.GetAll().Where(u => u.UserName != "super@site.com").To<UserViewModelSuper>();

            DataSourceResult result = model.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UsersEdit([DataSourceRequest]DataSourceRequest request, UserViewModelSuper model)
        {
            this.users.Update(model.Id, model.UserName, model.FirstName, model.LastName);

            var modelResult = this.users.GetAll().Where(u => u.UserName != "super@site.com").To<UserViewModelSuper>();

            DataSourceResult result = modelResult.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsersDelete([DataSourceRequest]DataSourceRequest request, UserViewModelSuper model)
        {
            this.users.Delete(model.Id);

            var modelResult = this.users.GetAll().Where(u => u.UserName != "super@site.com").To<UserViewModelSuper>();

            DataSourceResult result = modelResult.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}