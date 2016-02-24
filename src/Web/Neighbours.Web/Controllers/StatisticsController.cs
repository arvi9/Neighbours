namespace Neighbours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models.Home;
    using Services.Data.Contracts;

    public class StatisticsController : BaseController
    {
        private ICommunitiesService communities;
        private IUsersService users;
        private ICitiesService cities;

        public StatisticsController(ICommunitiesService communities, IUsersService users, ICitiesService cities)
        {
            this.communities = communities;
            this.users = users;
            this.cities = cities;
        }

        // GET: Statistics
        public ActionResult Index()
        {
            var citiesCount =
                this.Cache.Get(
                    "citiesCount",
                    () => this.cities.GetAll().Count(),
                 1 * 60);

            var communitiesCount =
                this.Cache.Get(
                    "communitiesCount",
                    () => this.communities.GetAll().Count(),
                 1 * 60);

            var usersCount =
               this.Cache.Get(
                   "usersCount",
                   () => this.users.GetAll().Count(),
                1 * 60);

            var statisctics = new StatisticsViewModel()
            {
                CitiesCount = citiesCount,
                CommunitiesCount = communitiesCount,
                UsersCount = usersCount
            };

            return this.PartialView("_StatisticsPartial", statisctics);
        }
    }
}