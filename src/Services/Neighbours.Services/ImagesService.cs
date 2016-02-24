namespace Neighbours.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Common.Extensions;
    using Contracts;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;

    public class ImagesService : IImagesService
    {
        private IRepository<ProfileImage> profileImages;
        private IRepository<CommunityImage> communityImages;
        private IRepository<PostImage> postImages;

        public ImagesService(IRepository<ProfileImage> profileImages, IRepository<CommunityImage> communityImages, IRepository<PostImage> postImages)
        {
            this.profileImages = profileImages;
            this.communityImages = communityImages;
            this.postImages = postImages;
        }

        public FileInfoModel GetDefualtProfileImage(Gender gender)
        {
            if (gender == Gender.Male)
            {
                return this.profileImages.GetById(1);
            }
            else if (gender == Gender.Female)
            {
                return this.profileImages.GetById(2);
            }
            else
            {
                return this.profileImages.GetById(3);
            }
        }

        public FileInfoModel GetDefualtCoverImage()
        {
            return this.communityImages.GetById(1);
        }

        public FileInfoModel GetProfileImage<T>(HttpPostedFileBase file, ImageType type)
            where T : FileInfoModel, new()
        {
            var name = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName).Remove(0, 1);

            var newImage = new T()
            {
                FileExtension = extension,
                OriginalFileName = name,
                NewFileName = name.ToMd5Hash() + "." + extension,
            };

            var path = $"~/Content/{type.ToString().ToLower()}/{newImage.Id.ToUrlPath()}";
            newImage.UrlPath = path;

            switch (type)
            {
                case ImageType.Profile:
                    this.profileImages.Add(newImage as ProfileImage);
                    this.profileImages.SaveChanges();
                    return newImage;
                case ImageType.Post:
                    this.postImages.Add(newImage as PostImage);
                    this.postImages.SaveChanges();
                    return newImage;
                case ImageType.Community:
                    this.communityImages.Add(newImage as CommunityImage);
                    this.communityImages.SaveChanges();
                    return newImage;
                default: return newImage;
            }
        }
    }
}
