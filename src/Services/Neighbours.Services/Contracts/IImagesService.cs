namespace Neighbours.Services.Data.Contracts
{
    using System.Web;
    using Common.Contracts;
    using Neighbours.Data.Models;

    public interface IImagesService : IService
    {
        FileInfoModel GetProfileImage<T>(HttpPostedFileBase file, ImageType type)
            where T : FileInfoModel, new();

        FileInfoModel GetDefualtProfileImage(Gender gender);

        FileInfoModel GetDefualtCoverImage();
    }
}
