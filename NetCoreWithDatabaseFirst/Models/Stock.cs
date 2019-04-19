using System;
using System.Collections.Generic;

namespace NetCoreWithDatabaseFirst.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int? ProductQty { get; set; }
        public string ProductStatus { get; set; }
    }
}
