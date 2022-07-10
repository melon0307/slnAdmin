using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CMemberViewModel
    {

        private Member _mem;

        public CMemberViewModel()
        {
            _mem = new Member();
        }

        public Member member
        {
            get { return _mem; }
            set { _mem = value; }
        }


        [DisplayName("手機號碼")]
        [Required]
        public string MemberPhone
        {
            get { return _mem.MemberPhone; }
            set { _mem.MemberPhone = value; }
        }

        [DisplayName("會員編號#")]
        public int MemberId
        {
            get { return _mem.MemberId; }
            //set { _mem.MemberId = value; }
        }

        public int ShoppingCarId
        {
            get { return _mem.ShoppingCarId; }
            set { _mem.ShoppingCarId = value; }
        }

        [DisplayName("電子郵件")]
        [Required]
        public string MemberEmail
        {
            get { return _mem.MemberEmail; }
            set { _mem.MemberEmail = value; }
        }

        [DisplayName("密碼")]
        [Required]
        public string MemberPassword
        {
            get { return _mem.MemberPassword; }
            set { _mem.MemberPassword = value; }
        }

        [DisplayName("地址")]
        [Required]
        public string MemberAddress
        {
            get { return _mem.MemberAddress; }
            set { _mem.MemberAddress = value; }
        }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "不得為空")]
        public string MemberName
        {
            get { return _mem.MemberName; }
            set { _mem.MemberName = value; }
        }

        [DisplayName("生日")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime MemberBirthDay
        {
            get { return _mem.MemberBirthDay; }
            set { _mem.MemberBirthDay = value; }
        }        

        [DisplayName("會員狀態")]
        public bool? BlackList
        {
            get { return _mem.BlackList; }
            set { _mem.BlackList = value; }
        }

        [DisplayName("會員照片")]
        public string MemberPhotoPath
        {
            get { return _mem.MemberPhotoPath; }
            set { _mem.MemberPhotoPath = value; }
        }
    }
}
