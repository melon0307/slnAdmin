using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string NewsDescription { get; set; }
        public DateTime? NewsStartTime { get; set; }
        public DateTime? NewsOverTime { get; set; }
    }
}
