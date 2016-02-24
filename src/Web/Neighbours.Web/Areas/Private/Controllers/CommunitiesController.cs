namespace Neighbours.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Common.Utilities;
    using Data;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Communities;
    using Services.Data.Contracts;

    public class CommunitiesController : BaseController
    {
        private ICommunitiesService communities;
        private IImagesService images;
        private IUsersService users;

        public CommunitiesController(ICommunitiesService communities, IImagesService images, IUsersService users)
        {
            this.communities = communities;
            this.images = images;
            this.users = users;
        }

        public ActionResult All()
        {
            var communities = this.communities.GetAll().To<CommunityViewModel>();
            return this.View(communities);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return this.Redirect("All");
            }

            var community = this.communities.Details(id);

            if (community == null)
            {
                return this.Redirect("All");
            }

            return this.View(this.Mapper.Map<CommunityDetailsViewModel>(community));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(CommunityAddViewModel model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var isImage = FileUtils.IsImage(file);

                if (!isImage)
                {
                    this.ModelState.AddModelError("ProfileImage", "The file should be an image");
                }

                var avatar = this.images.GetProfileImage<CommunityImage>(file, ImageType.Community);

                bool exists = Directory.Exists(this.Server.MapPath(avatar.UrlPath));

                if (!exists)
                {
                    Directory.CreateDirectory(this.Server.MapPath(avatar.UrlPath));
                }

                file.SaveAs(this.Server.MapPath(Path.Combine(avatar.UrlPath, avatar.NewFileName)));

                model.CommunityImageId = ((CommunityImage)avatar).Id;
            }
            else
            {
                model.CommunityImageId = ((CommunityImage)this.images.GetDefualtCoverImage()).Id;
            }

            var userId = this.User.Identity.GetUserId();

            var mapped = this.Mapper.Map<Community>(model);
            if (this.ModelState.IsValid)
            {
                this.communities.Add(mapped, model.City, model.District, userId);

                this.users.AddToRole(userId, "Administrator");

                return this.RedirectToAction("All", "Communities");
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Join(int id)
        {
            var userId = this.User.Identity.GetUserId();

            this.communities.Join(userId, id);

            var community = this.communities.GetAll().To<CommunityDetailsViewModel>().FirstOrDefault(c => c.Id == id);

            return this.PartialView("_PartialButtonsBadges", community);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var userId = this.User.Identity.GetUserId();

            this.communities.Cancel(userId, id);

            var community = this.communities.GetAll().To<CommunityDetailsViewModel>().FirstOrDefault(c => c.Id == id);

            return this.PartialView("_PartialButtonsBadges", community);
        }
    }
}