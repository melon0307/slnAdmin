using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CMemberDetailsViewModel
    {
        public CMemberViewModel member { get; set; }
        public List<COrderViewModel> order { get; set; }
    }
}
