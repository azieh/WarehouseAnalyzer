using System.Collections.Generic;
using Common.Models;

namespace Common.Interfaces
{
    public interface IDataOperator
    {
        void WriteDataToConsole(List<WarehouseDetails> input);
        List<WarehouseDetails> MapProductInfoToWarehousePerspective(List<WarehouseItemDetails> input);
        List<WarehouseItemDetails> MapProductInfoToWarehouseProductInfoPerspective(List<InputMaterialData> input);
    }
}