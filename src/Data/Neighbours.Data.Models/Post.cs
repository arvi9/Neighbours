using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data.Models
{
    public class Post
    {
        
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public int PostImageId { get; set; }

        public virtual PostImage PostImage { get; set; }

        public int CommunityId { get; set; }

        public virtual Community Community { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }
    }
}
