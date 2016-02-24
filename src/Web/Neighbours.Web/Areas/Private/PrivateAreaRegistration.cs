﻿namespace Neighbours.Web.Areas.Private
{
    using System.Web.Mvc;

    public class PrivateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Private";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Private_default",
                "Private/{controller}/{action}/{id}",
                new { controller = "Communities", action = "Index", id = UrlParameter.Optional });
        }
    }
}