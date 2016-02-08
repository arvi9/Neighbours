using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data.Models
{
    [Table("ProfileImages")]
    public class ProfileImage : FileInfo
    {
        //public string UserId { get; set; }

        //[InverseProperty("ProfileImage")]
        //public virtual User User { get; set; }
    }
}
