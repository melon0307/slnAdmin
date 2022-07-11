using Microsoft.AspNetCore.Http;
using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CArticleViewModel
    {

        public CArticleViewModel()//建構子
        {
            _art = new Article();//將Article物件注入全域變數_art中
        }
        private Article _art;
        public Article article
        {
            get { return _art; }
            set { _art = value; }
        }

        public int ArticleId
        {
            get { return _art.ArticleId; }//透過全域變數_prod呼叫Article物件的屬性
            set { _art.ArticleId = value; }
        }
        [DisplayName("文章名稱")]
        public string ArticleName
        {
            get { return _art.ArticleName; }
            set { _art.ArticleName = value; }
        }
        [DisplayName("文章內容")]
        public string ArticleDescription
        {
            get { return _art.ArticleDescription; }
            set { _art.ArticleDescription = value; }
        }
        [DisplayName("圖片路徑")]
        public string ArticleImage
        {
            get { return _art.ArticleImage; }
            set { _art.ArticleImage = value; }
        }
        [DisplayName("文章時間")]
        public DateTime? ArticleDate
        {
            get { return _art.ArticleDate; }
            set { _art.ArticleDate = value; }
        }
        public IFormFile photo { get; set; }
    }
}
