using System;
using System.Collections.Generic;

namespace mtpd.Models
{
    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? PricePerUnit { get; set; }
        public string Unit { get; set; }
        public bool? Active { get; set; }
        public decimal? QuantityInStock { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
