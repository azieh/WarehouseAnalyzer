using Common.Interfaces;

namespace Warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            var warehouseAnalyzer = new RegisterContainers().GetInitialServiceService();

            warehouseAnalyzer.Start();
        }
    }
}
