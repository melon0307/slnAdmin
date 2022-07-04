using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace prjAdmin.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [DisplayName("類別")]
        public string CategoriesName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
