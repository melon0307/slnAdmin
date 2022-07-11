using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CAdimnViewModel
    {
        public int AdminId { get; set; }
        [Remote(action: "IfEmailExist",controller:"Api",HttpMethod ="POST")]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ProductOk { get; set; }
        public bool OrderOk { get; set; }
        public bool MemberOk { get; set; }
        public bool ArticleOk { get; set; }
        public bool AdminOk { get; set; }
    }
}
