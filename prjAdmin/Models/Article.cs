using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Article
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleImage { get; set; }
        public DateTime? ArticleDate { get; set; }
    }
}
