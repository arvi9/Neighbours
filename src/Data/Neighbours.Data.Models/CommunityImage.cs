using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data.Models
{
    [Table("CommunityImages")]
    public class CommunityImage : FileInfo
    {
        //public int CommunityId { get; set; }

        //[InverseProperty("CommunityImage")]
        //public virtual Community Community { get; set; }
    }
}
