namespace Neighbours.Services.Common.Contracts
{
    using System.Web;
    using Data.Models;

    public interface IImagesService : IService
    {
        ProfileImage GetProfileImage(HttpPostedFileBase file);
    }
}
