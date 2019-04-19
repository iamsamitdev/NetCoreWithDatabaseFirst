using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWithDatabaseFirst.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนชื่อสินค้าก่อน")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนราคาก่อน")]
        [Range(minimum: 0, maximum: 10000, ErrorMessage = "ป้อนข้อมูลตั้งแต่ 0-10,000 เท่านั้น")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
