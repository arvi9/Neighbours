using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data.Models
{
    [Table("CommentImages")]
    public class CommentImage : FileInfo
    {
        //public int CommentId { get; set; }

        //[InverseProperty("CommentImage")]
        //public virtual Comment Comment { get; set; }
    }
}
