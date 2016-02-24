namespace Neighbours.Web.Models.Communities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Common;
    using Services.Common.Contracts;

    public class CommunityViewModel : IMapFrom<Community>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"{identifier.EncodeId(this.Id)}";
            }
        }

        public int CommunityImageId { get; set; }

        public CommunityImage CommunityImage { get; set; }

        public ICollection<User> Users { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Community, CommunityViewModel>()
                   .ForMember(c => c.City, opts => opts.MapFrom(c => c.City.Name))
                   .ForMember(c => c.District, opts => opts.MapFrom(c => c.District.Name));
        }
    }
}