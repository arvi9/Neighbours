namespace Neighbours.Web.Areas.SuperUser.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Neighbours.Data.Models;
    using Neighbours.Web.Infrastructure.Mapping;

    public class UserViewModelSuper : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModelSuper>()
                .ForMember(u => u.ProfileImage, opts => opts.MapFrom(g => g.ProfileImage.UrlPath.Remove(0, 1) + "/" + g.ProfileImage.NewFileName));
        }
    }
}