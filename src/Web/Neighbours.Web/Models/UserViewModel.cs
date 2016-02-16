namespace Neighbours.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Neighbours.Data.Models;
    using Neighbours.Web.Infrastructure.Mapping;

    public class UserViewModel : Profile
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        protected override void Configure()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(u => u.Id, opts => opts.MapFrom(g => g.Id))
                .ForMember(u => u.UserName, opts => opts.MapFrom(g => g.UserName));
        }
    }
}