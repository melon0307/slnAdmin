using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class ShoppingCar
    {
        public ShoppingCar()
        {
            ShoppingCarDetails = new HashSet<ShoppingCarDetail>();
        }

        public int ShoppinCarId { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<ShoppingCarDetail> ShoppingCarDetails { get; set; }
    }
}
