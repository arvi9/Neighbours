namespace Neighbours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Models.Communities;
    using Neighbours.Data.Models;
    using Neighbours.Services.Data.Contracts;
    using Neighbours.Web.Models;

    public class HomeController : BaseController
    {
        public const int RecordsPerPage = 6;
        private IUsersService users;
        private ICommunitiesService communities;

        public HomeController(IUsersService users, ICommunitiesService communities)
        {
            this.users = users;
            this.communities = communities;
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("GetCommunities");
        }

        public ActionResult All()
        {
            var communities = this.communities.GetAll().To<CommunityViewModel>();
            return this.View(communities);
        }

        public ActionResult GetCommunities(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            if (this.Request.IsAjaxRequest())
            {
                var communities = this.GetRecordsForPage(pageNum.Value);
                return this.PartialView("_CommunityRow", communities);
            }
            else
            {
                this.LoadAllCommunitiesToCache();
                var firstList = this.GetRecordsForPage(0);
                return this.View("Index", firstList);
            }
        }

        public void LoadAllCommunitiesToCache()
        {
            var communities =
               this.Cache.Get(
                   "Communities",
                   () => this.communities.GetAll().To<CommunityViewModel>().ToList(),
                1 * 60);
        }

        public IEnumerable<CommunityViewModel> GetRecordsForPage(int pageNum)
        {
            var communities =
               this.Cache.Get(
                   "Communities",
                   () => this.communities.GetAll().To<CommunityViewModel>().ToList(),
                1 * 60);

            int from = pageNum * RecordsPerPage;
            int to = from + RecordsPerPage;

            return communities
                .OrderBy(x => x.Id)
                .Skip(from)
                .Take(RecordsPerPage);
        }
    }
}