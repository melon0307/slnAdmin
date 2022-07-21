using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Package
    {
        public Package()
        {
            Coffees = new HashSet<Coffee>();
        }

        public int PackageId { get; set; }
        [DisplayName("包裝法")]
        public string PackageName { get; set; }

        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
