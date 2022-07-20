using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Order
    {
        public Order()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStateId { get; set; }
        public int PaymentId { get; set; }
        public string OrderAddress { get; set; }
        public string OrderReceiver { get; set; }
        public string OrderPhone { get; set; }
        public int? CouponId { get; set; }
        public decimal? Fee { get; set; }
        public string TradeNo { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Member Member { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
