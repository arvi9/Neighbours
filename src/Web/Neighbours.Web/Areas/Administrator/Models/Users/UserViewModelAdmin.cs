namespace Neighbours.Web.Areas.Administrator.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Neighbours.Data.Models;
    using Neighbours.Web.Infrastructure.Mapping;

    public class UserViewModelAdmin : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public string ForUpdate { get; set; }

        public List<string> Communities { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModelAdmin>()
                .ForMember(u => u.Communities, opts => opts.Ignore())
                .ForMember(u => u.ProfileImage, opts => opts.MapFrom(g => g.ProfileImage.UrlPath.Remove(0, 1) + "/" + g.ProfileImage.NewFileName));
        }
    }
}