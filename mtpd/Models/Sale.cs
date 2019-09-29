using System;
using System.Collections.Generic;

namespace mtpd.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? QuantitySold { get; set; }
        public decimal? Price { get; set; }

        public virtual Product Product { get; set; }
    }
}
