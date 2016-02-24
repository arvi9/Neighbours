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
    using Neighbours.Common.GlobalConstants;

    public class CommunityAddViewModel : IMapFrom<Community>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalValidationConstants.MaxCommunityNameLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = GlobalValidationConstants.MinCommunityNameLength)]
        public string Name { get; set; }

        public Status Status { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public int CommunityImageId { get; set; }

        public CommunityImage CommunityImage { get; set; }

        [Required]
        [StringLength(GlobalValidationConstants.MaxCommunityAddressLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = GlobalValidationConstants.MinCommunityAddressLength)]
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<CommunityAddViewModel, Community>()
                .ForMember(c => c.City, opts => opts.Ignore())
                .ForMember(c => c.District, opts => opts.Ignore());
        }
    }
}