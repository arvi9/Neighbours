namespace Neighbours.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Like
    {
        public int Id { get; set; }

        public LikeType Type { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
