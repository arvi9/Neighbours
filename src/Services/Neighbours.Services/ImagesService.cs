namespace Neighbours.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;
    using Neighbours.Services.Common.Contracts;

    public class ImagesService : IImagesService
    {
        private IRepository<ProfileImage> images;

        public ImagesService(IRepository<ProfileImage> images)
        {
            this.images = images;
        }

        public ProfileImage GetProfileImage(HttpPostedFileBase file)
        {
            var name = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);


            var path = "~/Content/imgs/";
            var newImage = new ProfileImage()
            {
                FileExtension = extension,
                OriginalFileName = name,
                UrlPath = path
            };



            this.images.Add(newImage);
            this.images.SaveChanges();

            

            return newImage;
        }
    }
}
