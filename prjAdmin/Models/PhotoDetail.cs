using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class PhotoDetail
    {
        public int PhotoDetailsId { get; set; }
        public int ProductId { get; set; }
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual Product Product { get; set; }
    }
}
