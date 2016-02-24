namespace Neighbours.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Neighbours.Data.Models;
    using Neighbours.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public ProfileImage ProfileImage { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(u => u.Id, opts => opts.MapFrom(g => g.Id))
                .ForMember(u => u.UserName, opts => opts.MapFrom(g => g.UserName));
        }
    }
}