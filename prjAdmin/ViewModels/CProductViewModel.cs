using Microsoft.AspNetCore.Http;
using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CProductViewModel
    {
        public int ProductId { get; set; }
        [DisplayName("產品名稱")]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int? CountryId { get; set; }
        [DisplayName("售價")]
        public decimal? Price { get; set; }
        [DisplayName("產品介紹")]
        public string Description { get; set; }
        [DisplayName("庫存量")]
        public int? Stock { get; set; }
        [DisplayName("點擊數")]
        public int? ClickCount { get; set; }
        [DisplayName("產品狀態")]
        public bool TakeDown { get; set; }
        [DisplayName("產品評價")]
        public double? Star { get; set; }
        public int RoastingId { get; set; }
        public int ProcessId { get; set; }
        public int PackageId { get; set; }        
        [DisplayName("是否加入雨林聯盟")]
        public bool RainForest { get; set; }
        public Category Category { get; set; }
        public Country Country { get; set; }
        public Coffee Coffee { get; set; }
        public IFormFile photo { get; set; }
        public string MainPhotoPath { get; set; }
    }
}
