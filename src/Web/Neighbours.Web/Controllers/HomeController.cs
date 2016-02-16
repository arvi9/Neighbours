namespace Neighbours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Neighbours.Data.Models;
    using Neighbours.Services.Data.Contracts;
    using Neighbours.Web.Models;

    public class HomeController : Controller
    {
        private IUserServices users;
        private IMapper mapper;

        public HomeController(IUserServices users, IMapper mapper)
        {
            this.users = users;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var user = this.users.GetById("cd6eeb2f-4bc3-4c0e-a991-23d748e072ec");

            var dto = this.mapper.Map<User, UserViewModel>(user);

            this.ViewBag.User = dto;
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}