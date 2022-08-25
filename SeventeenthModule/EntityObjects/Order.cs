using System;
using System.Collections.Generic;

namespace SeventeenthModule.EntityObjects
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? ProductId { get; set; }
        public int? Clientid { get; set; }

        public virtual EntityClient? Client { get; set; }
        public virtual Product? Product { get; set; }
    }
}
