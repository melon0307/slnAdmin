using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Awesomes = new HashSet<Awesome>();
        }

        public int CommentId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int MemberId { get; set; }
        public int? CommentParentId { get; set; }
        public string CommentDescription { get; set; }
        public double? Star { get; set; }

        public virtual Member Member { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Awesome> Awesomes { get; set; }
    }
}
