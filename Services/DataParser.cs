using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Common.Interfaces;
using Common.Models;

namespace Services
{
    public class DataParser : IDataParser
    {
        private readonly string _regexOneInputLinePattern = "([^;]*)";
        private readonly int _regexExpectedResultArrayLength = 13;

        private readonly string _splitWarehouseDetailsSymbol = "|";
        private readonly string _splitWarehouseItemSymbol = ",";

        public List<ItemDetails> DeserializeToWarehouseItems(string inputData)
        {
            List<ItemDetails> warehouseItems = new List<ItemDetails>();
            var result = Regex.Split(inputData, _regexOneInputLinePattern);

            if (result.Length < _regexExpectedResultArrayLength)
                throw new ArgumentException("Input value is not valid");

            foreach (var warehouseItem in result[9].Split(_splitWarehouseDetailsSymbol))
            {
                var item = warehouseItem.Split(_splitWarehouseItemSymbol);

                warehouseItems.Add(new ItemDetails
                {
                    Name = item[0],
                    Quantity = int.Parse(item[1])
                });
            }

            return warehouseItems;
        }

        public InputMaterialData DeserializeToInputMaterial(string inputData)
        {
            var result = Regex.Split(inputData, _regexOneInputLinePattern);

            if (result.Length < _regexExpectedResultArrayLength)
                throw new ArgumentException("Input value is not valid");

            return new InputMaterialData { Id = result[5].Trim(), Name = result[1].Trim() };
        }

        public List<InputMaterialData> DeserializeToCompleteList(List<string> inputData)
        {
            List<InputMaterialData> result = new List<InputMaterialData>();
            foreach (var input in inputData)
            {
                var deserializedInput = DeserializeToInputMaterial(input);
                deserializedInput.Warehouse = DeserializeToWarehouseItems(input);
                result.Add(deserializedInput);
            }

            return result;
        }
    }
}