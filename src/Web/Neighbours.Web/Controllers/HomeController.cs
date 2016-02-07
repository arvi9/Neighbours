using AutoMapper;
using Neighbours.Data.Models;
using Neighbours.Services.Data.Contracts;
using Neighbours.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neighbours.Web.Controllers
{
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

            var dto = mapper.Map<User, UserViewModel>(user);

            ViewBag.User = dto;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}