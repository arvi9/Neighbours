namespace Neighbours.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Neighbours.Common.GlobalConstants;
    using Neighbours.Common.ValidationAttributes;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Post> posts;
        private ICollection<Community> communities;
        private ICollection<Like> likes;

        public User()
        {
            this.posts = new HashSet<Post>();
            this.communities = new HashSet<Community>();
            this.likes = new HashSet<Like>();
            this.WaitingList = "|";
        }

        [Required]
        [DisplayName("First name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [DisplayName("Date of birth")]
        [BirthDate(GlobalValidationConstants.MinBirthDate, GlobalValidationConstants.MinAgeToRegister, ErrorMessage = "You are either too old or too young.")]
        public DateTime? BirthDate { get; set; }

        public int ProfileImageId { get; set; }

        [Display(Name = "Profile Image")]
        public virtual ProfileImage ProfileImage { get; set; }

        public Gender Gender { get; set; }

        public string WaitingList { get; set; }

        [NotMapped]
        public int[] WaitingListCommunities
        {
            get
            {
                return this.WaitingList.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }

            set
            {
                this.WaitingList = string.Join("|", value);
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

        public virtual ICollection<Community> Communities
        {
            get
            {
                return this.communities;
            }

            set
            {
                this.communities = value;
            }
        }

        public virtual ICollection<Like> Likes
        {
            get
            {
                return this.likes;
            }

            set
            {
                this.likes = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
