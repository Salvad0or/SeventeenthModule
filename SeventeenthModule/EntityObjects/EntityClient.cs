using System;
using System.Collections.Generic;

namespace SeventeenthModule.EntityObjects
{
    public partial class EntityClient
    {
        public EntityClient()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Pname { get; set; } = null!;
        public string? Phone { get; set; }
        public string Emai { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
