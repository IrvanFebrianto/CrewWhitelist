using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    [MetadataType(typeof(ProfileMetadata))]
    public partial class Profile
    {
    }

    public class ProfileMetadata
    {
        [Key]
        public System.Guid UserId { get; set; }

        //[ForeignKey("ApplicationId")]
        //public virtual Application Application { get; set; }
    }
}
