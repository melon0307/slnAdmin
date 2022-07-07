using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public int? ProductId { get; set; }
        public string ImagePath { get; set; }

        public virtual Product Product { get; set; }
    }
}
