﻿using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ProductOk { get; set; }
        public bool OrderOk { get; set; }
        public bool MemberOk { get; set; }
        public bool ArticleOk { get; set; }
        public bool AdminOk { get; set; }
    }
}
