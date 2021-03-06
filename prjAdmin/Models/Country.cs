using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Country
    {
        public Country()
        {
            Coffees = new HashSet<Coffee>();
            Products = new HashSet<Product>();
        }

        public int CountryId { get; set; }
        [DisplayName("國家")]
        public string CountryName { get; set; }
        public int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
        public virtual ICollection<Coffee> Coffees { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
