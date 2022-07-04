using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Qquestionnaire
    {
        public int QquestionnaireId { get; set; }
        public int MemberId { get; set; }
        public int CouponId { get; set; }
        public int? QuestionTableId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Member Member { get; set; }
        public virtual QuestionTable QuestionTable { get; set; }
    }
}
