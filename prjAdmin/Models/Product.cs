using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            MyLikes = new HashSet<MyLike>();
            OrderDetails = new HashSet<OrderDetail>();
            PhotoDetails = new HashSet<PhotoDetail>();
            ShoppingCarDetails = new HashSet<ShoppingCarDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int? CountryId { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public int? ClickCount { get; set; }
        public bool TakeDown { get; set; }
        public double? Star { get; set; }

        public virtual Category Category { get; set; }
        public virtual Country Country { get; set; }
        public virtual Coffee Coffee { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MyLike> MyLikes { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PhotoDetail> PhotoDetails { get; set; }
        public virtual ICollection<ShoppingCarDetail> ShoppingCarDetails { get; set; }
    }
}
