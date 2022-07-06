using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Roasting
    {
        public Roasting()
        {
            Coffees = new HashSet<Coffee>();
        }

        public int RoastingId { get; set; }
        [DisplayName("烘培法")]
        public string RoastingName { get; set; }

        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
