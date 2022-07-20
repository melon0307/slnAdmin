using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class COrderViewModel2
    {
        private Order _ord;

        public COrderViewModel2()
        {
            _ord = new Order();
        }

        public Order order
        {
            get { return _ord; }
            set { _ord = value; }
        }

        [DisplayName("訂單編號")]
        public int OrderId
        {
            get { return _ord.OrderId; }
            set { _ord.OrderId = value; }
        }

        [DisplayName("會員編號")]
        [Required]
        public int MemberId
        {
            get { return _ord.MemberId; }
            set { _ord.MemberId = value; }
        }

        [DisplayName("成立日期")]
        [Required]
        public DateTime OrderDate
        {
            get { return _ord.OrderDate; }
            set { _ord.OrderDate = value; }
        }

        [DisplayName("訂單狀態")]
        [Required]
        public int OrderStateId
        {
            get { return _ord.OrderStateId; }
            set { _ord.OrderStateId = value; }
        }

        [DisplayName("支付方式")]
        [Required]
        public int PaymentId
        {
            get { return _ord.PaymentId; }
            set { _ord.PaymentId = value; }
        }

        [DisplayName("訂單地址")]
        [Required]
        public string OrderAddress
        {
            get { return _ord.OrderAddress; }
            set { _ord.OrderAddress = value; }
        }

        [DisplayName("收件者姓名")]
        [Required]
        public string OrderReceiver
        {
            get { return _ord.OrderReceiver; }
            set { _ord.OrderReceiver = value; }
        }

        [DisplayName("收件者電話")]
        [Required]
        public string OrderPhone
        {
            get { return _ord.OrderPhone; }
            set { _ord.OrderPhone = value; }
        }

        [DisplayName("折價券")]
        public int? CouponId
        {
            get { return _ord.CouponId; }
            set { _ord.CouponId = value; }
        }

        [DisplayName("運費")]
        public decimal Fee
        {
            get { return (decimal)_ord.Fee; }
            set { _ord.Fee = value; }
        }

        [DisplayName("訂單編號")]
        public string TradeNo
        {
            get { return _ord.TradeNo; }
            set { _ord.TradeNo = value; }
        }

        public Payment Payment { get; set; }
        public Coupon Coupon { get; set; }
        public OrderState OrderState { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
