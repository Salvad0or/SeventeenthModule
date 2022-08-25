using System;
using System.Collections.Generic;

namespace SeventeenthModule.EntityObjects
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Firm { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public decimal? Weight { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
