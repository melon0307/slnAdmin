using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class ShoppingCarDetail
    {
        public int ShoppingCarDetialsId { get; set; }
        public int MemberId { get; set; }
        public int? ProductsId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual ShoppingCar Member { get; set; }
        public virtual Product Products { get; set; }
    }
}
