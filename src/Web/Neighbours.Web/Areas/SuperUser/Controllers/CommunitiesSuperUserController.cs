namespace Neighbours.Web.Areas.SuperUser.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CommunitiesSuperUserController : Controller
    {
        // GET: SuperUser/CommunitiesSuperUser
        public ActionResult Index()
        {
            return this.View();
        }
    }
}