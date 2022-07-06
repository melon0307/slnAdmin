using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Process
    {
        public Process()
        {
            Coffees = new HashSet<Coffee>();
        }

        public int ProcessId { get; set; }
        [DisplayName("處理法")]
        public string ProcessName { get; set; }

        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
