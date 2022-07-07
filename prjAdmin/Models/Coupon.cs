using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponDetails = new HashSet<CouponDetail>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public decimal Money { get; set; }
        public int Condition { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponDeadline { get; set; }

        public virtual ICollection<CouponDetail> CouponDetails { get; set; }
    }
}
