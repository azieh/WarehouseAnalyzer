using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Common.Interfaces;
using Common.Models;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class DataOperator : IDataOperator
    {
        private readonly Lazy<ILogger<DataOperator>> _loggerLazy;

        public DataOperator(ILogger<DataOperator> logger)
        {
            if(logger is null)
                throw new ArgumentNullException(nameof(logger));

            _loggerLazy = new Lazy<ILogger<DataOperator>>(() => logger);
        }

        public List<WarehouseItemDetails> MapProductInfoToWarehouseProductInfoPerspective(List<InputMaterialData> input)
        {
            List<WarehouseItemDetails> warehouseItemsDetail = new List<WarehouseItemDetails>();
            foreach (var productData in input)
            {
                List<WarehouseItemDetails> warehouseDetails = productData.Warehouse
                  .Select(_ =>
                      new WarehouseItemDetails
                      {
                          Quantity = _.Quantity,
                          WarehouseName = _.Name
                      })
                  .ToList();

                warehouseDetails.ForEach(_ =>
                {
                    _.ProductId = productData.Id;
                    _.ProductName = productData.Name;
                });
                warehouseItemsDetail.AddRange(warehouseDetails);
            }

            return warehouseItemsDetail;
        }

        public List<WarehouseDetails> MapProductInfoToWarehousePerspective(List<WarehouseItemDetails> input)
        {

            List<WarehouseDetails> warehouseItemsDetail = new List<WarehouseDetails>();
            foreach (var productData in input.GroupBy(_ => _.WarehouseName))
            {

                List<ItemDetails> warehouseItemDetails = productData
                    .Select(_ =>
                        new ItemDetails()
                        {
                            Name = _.ProductId,
                            Quantity = _.Quantity
                        })
                    .OrderBy(_ => _.Name)
                    .ToList();

                int totalQuantity = warehouseItemDetails.Sum(_ => _.Quantity);
                warehouseItemsDetail.Add(new WarehouseDetails
                {
                    WarehouseName = productData.Key,
                    TotalQuantity = totalQuantity,
                    Items = warehouseItemDetails
                });
            }

            return warehouseItemsDetail
                .OrderByDescending(_ => _.TotalQuantity)
                .ThenByDescending(_ => _.WarehouseName)
                .ToList();
        }

        public void WriteDataToConsole(List<WarehouseDetails> input)
        {
            var logger = _loggerLazy.Value;
            foreach (var warehouseDetails in input)
            {
                logger.LogInformation($"{warehouseDetails.WarehouseName} (total {warehouseDetails.TotalQuantity})");

                warehouseDetails.Items.ForEach(_ => logger.LogInformation($"{_.Name}: {_.Quantity}"));
                logger.LogInformation(string.Empty);
            }
        }
    }
}