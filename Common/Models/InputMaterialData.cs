using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class InputMaterialData
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public List<ItemDetails> Warehouse { get; set; }
    }
}
