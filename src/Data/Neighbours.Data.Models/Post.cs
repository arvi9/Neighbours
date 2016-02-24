namespace Neighbours.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        private ICollection<Comment> comments;
        private ICollection<Like> likes;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        public int? PostImageId { get; set; }

        [Display(Name = "Post Image")]
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
    }
}
