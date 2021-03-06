﻿namespace Neighbours.Web.Areas.Administrator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Models.Communities;
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

        // GET: Administrator/Communities
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult UsersRead([DataSourceRequest]DataSourceRequest request)
        {
            var id = this.Request.UrlReferrer.Segments[this.Request.UrlReferrer.Segments.Count() - 1];

            var model = this.users.GetAllPending(int.Parse(id)).To<UserViewModelAdmin>();

            DataSourceResult result = model.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UsersAccept([DataSourceRequest]DataSourceRequest request, UserViewModelAdmin model)
        {
            var id = this.Request.UrlReferrer.Segments[this.Request.UrlReferrer.Segments.Count() - 1];
            var userId = model.Id;
            this.users.AddToCommunity(userId, int.Parse(id));

            var modelResult = this.users.GetAllPending(int.Parse(id)).To<UserViewModelAdmin>();

            DataSourceResult result = modelResult.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsersDeny([DataSourceRequest]DataSourceRequest request, UserViewModelAdmin model)
        {
            var id = this.Request.UrlReferrer.Segments[this.Request.UrlReferrer.Segments.Count() - 1];
            var userId = model.Id;
            this.communities.Cancel(userId, int.Parse(id));

            var modelResult = this.users.GetAllPending(int.Parse(id)).To<UserViewModelAdmin>();

            DataSourceResult result = modelResult.ToDataSourceResult(request);

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}