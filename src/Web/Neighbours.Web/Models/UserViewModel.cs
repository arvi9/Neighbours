using Neighbours.Data.Models;
using Neighbours.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Neighbours.Web.Models
{
    public class UserViewModel : Profile    /*, IMapFrom<User>, IHaveCustomMappings*/ // Static API before 4.2.0
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        //// Static API before 4.2.0
        //public void CreateMappings(IMapperConfiguration config)
        //{
        //    config.CreateMap<User, UserViewModel>()
        //        .ForMember(u => u.Id, opts => opts.MapFrom(g => g.Id))
        //        .ForMember(u => u.UserName, opts => opts.MapFrom(g => g.UserName));
        //}

        protected override void Configure()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(u => u.Id, opts => opts.MapFrom(g => g.Id))
                .ForMember(u => u.UserName, opts => opts.MapFrom(g => g.UserName));
        }
    }
}