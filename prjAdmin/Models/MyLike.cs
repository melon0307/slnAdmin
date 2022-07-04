using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class MyLike
    {
        public int LikeId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
