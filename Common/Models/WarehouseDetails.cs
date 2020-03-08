using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class WarehouseDetails
    {
        public int TotalQuantity { get; set; }
        public string WarehouseName { get; set; }
        public List<ItemDetails> Items { get; set; }
    }
}
