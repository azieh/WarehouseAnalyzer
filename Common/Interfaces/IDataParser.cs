using System.Collections.Generic;
using Common.Models;

namespace Common.Interfaces
{
    public interface IDataParser
    {
        List<ItemDetails> DeserializeToWarehouseItems(string inputData);
        InputMaterialData DeserializeToInputMaterial(string inputData);
        List<InputMaterialData> DeserializeToCompleteList(List<string> inputData);
    }
}