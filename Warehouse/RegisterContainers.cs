using Common;
using Common.Interfaces;
using Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services;

namespace Warehouse
{
    public class RegisterContainers
    {
        private readonly ServiceProvider _serviceProvider;
    
        public RegisterContainers()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(configure => 
                    configure
                        .ClearProviders()
                        .AddProvider(new LoggerProvider()))
                .AddSingleton<IWarehouseFacade, WarehouseFacade>()
                .AddSingleton<IInputReader, InputReader>()
                .AddSingleton<IDataOperator, DataOperator>()
                .AddSingleton<IDataParser, DataParser>()
                .BuildServiceProvider();
        }

        public IWarehouseFacade GetInitialServiceService() => _serviceProvider.GetService<IWarehouseFacade>();
    }
}