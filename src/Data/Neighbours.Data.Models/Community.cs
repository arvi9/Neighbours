namespace Neighbours.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Community
    {
        private ICollection<User> users;
        private ICollection<Post> posts;

        public Community()
        {
            this.users = new HashSet<User>();
            this.posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual City City { get; set; }

        public virtual District District { get; set; }

        public int CommunityImageId { get; set; }

        public virtual CommunityImage CommunityImage { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

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
