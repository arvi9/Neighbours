namespace Neighbours.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Neighbours.Common.GlobalConstants;

    public class Community : DeletableEntity
    {
        private ICollection<User> users;
        private ICollection<Post> posts;
        private ICollection<User> admins;

        public Community()
        {
            this.users = new HashSet<User>();
            this.posts = new HashSet<Post>();
            this.admins = new HashSet<User>();
            this.WaitingUsers = "|";
        }

        public int Id { get; set; }

        [StringLength(GlobalValidationConstants.MaxCommunityNameLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = GlobalValidationConstants.MinCommunityNameLength)]
        public string Name { get; set; }

        public virtual City City { get; set; }

        public virtual District District { get; set; }

        public int CommunityImageId { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public string WaitingUsers { get; set; }

        [NotMapped]
        public string[] WaitingUsersList
        {
            get
            {
                return this.WaitingUsers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }

            set
            {
                this.WaitingUsers = string.Join("|", value);
            }
        }

        [Display(Name = "Community Image")]
        public virtual CommunityImage CommunityImage { get; set; }

        public virtual Status Status { get; set; }

        [StringLength(GlobalValidationConstants.MaxCommunityAddressLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = GlobalValidationConstants.MinCommunityAddressLength)]
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<User> Users
        {
            get
            {
                return this.users;
            }

            set
            {
                this.users = value;
            }
        }

        public virtual ICollection<User> Admins
        {
            get
            {
                return this.admins;
            }

            set
            {
                this.admins = value;
            }
        }

        public virtual ICollection<Post> Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
            }
        }
    }
}
