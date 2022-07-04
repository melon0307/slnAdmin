using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Photo
    {
        public Photo()
        {
            PhotoDetails = new HashSet<PhotoDetail>();
        }

        public int PhotoId { get; set; }
        public byte[] Photo1 { get; set; }

        public virtual ICollection<PhotoDetail> PhotoDetails { get; set; }
    }
}
