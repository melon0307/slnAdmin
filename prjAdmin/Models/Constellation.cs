using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Constellation
    {
        public Constellation()
        {
            Coffees = new HashSet<Coffee>();
        }

        public int ConstellationId { get; set; }
        public string ConstellationName { get; set; }
        public string ConstellationDescription { get; set; }
        public byte[] ConstellationPhoto { get; set; }
        public int? ConstellationProductId { get; set; }

        public virtual ICollection<Coffee> Coffees { get; set; }
    }
}
