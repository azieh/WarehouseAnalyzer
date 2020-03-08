using System;
using System.Collections.Generic;
using Common;
using Common.Interfaces;
using Common.Models;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class WarehouseFacade : IWarehouseFacade
    {
        private readonly Lazy<IInputReader> _inputReaderLazy;
        private readonly Lazy<IDataParser> _dataParserLazy;
        private readonly Lazy<IDataOperator> _dataOperator;
        public WarehouseFacade(IInputReader inputReader, IDataParser dataParser, IDataOperator dataOperator)
        {
            if (inputReader is null)
                throw new ArgumentNullException(nameof(inputReader));
            if (dataParser is null)
                throw new ArgumentNullException(nameof(dataParser));
            if (dataOperator is null)
                throw new ArgumentNullException(nameof(dataOperator));

            _inputReaderLazy = new Lazy<IInputReader>(() => inputReader);
            _dataParserLazy = new Lazy<IDataParser>(() => dataParser);
            _dataOperator = new Lazy<IDataOperator>(() => dataOperator);
        }
        public void Start()
        {
            while (true) // Loop indefinitely
            {
                List<string> stdIn = _inputReaderLazy.Value.ReadInputData();
                List<InputMaterialData> inputMaterialData = _dataParserLazy.Value.DeserializeToCompleteList(stdIn);
                var productInfo =
                    _dataOperator.Value.MapProductInfoToWarehouseProductInfoPerspective(inputMaterialData);
                var warehouseInfo = _dataOperator.Value.MapProductInfoToWarehousePerspective(productInfo);
                _dataOperator.Value.WriteDataToConsole(warehouseInfo);
            }
        }
    }
}
