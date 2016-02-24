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

    public class CommunityDetailsViewModel : IMapFrom<Community>, IHaveCustomMappings
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

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<User> Admins { get; set; }

        public ICollection<Post> Posts { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Community, CommunityDetailsViewModel>()
                   .ForMember(c => c.City, opts => opts.MapFrom(c => c.City.Name))
                   .ForMember(c => c.District, opts => opts.MapFrom(c => c.District.Name));
        }
    }
}