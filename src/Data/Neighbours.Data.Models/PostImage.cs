using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data.Models
{
    [Table("PostImages")]
    public class PostImage : FileInfo
    {
        //public int PostId { get; set; }

        //[InverseProperty("PostImage")]
        //public virtual Post Post { get; set; }
    }
}
