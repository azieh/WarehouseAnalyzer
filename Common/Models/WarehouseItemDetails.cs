using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class WarehouseItemDetails
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string WarehouseName { get; set; }
        public string ProductName { get; set; }
    }
}
