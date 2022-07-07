using System;
using System.Collections.Generic;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Coffee
    {
        public int CoffeeId { get; set; }
        public int ProductId { get; set; }
        public string CoffeeName { get; set; }
        public int CountryId { get; set; }
        public int RoastingId { get; set; }
        public int ProcessId { get; set; }
        public int PackageId { get; set; }
        public bool RainForest { get; set; }

        public virtual Country Country { get; set; }
        public virtual Package Package { get; set; }
        public virtual Process Process { get; set; }
        public virtual Product Product { get; set; }
        public virtual Roasting Roasting { get; set; }
    }
}
