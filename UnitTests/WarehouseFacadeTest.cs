using System;
using System.IO;
using System.Linq;
using Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Services;
using ILogger = Castle.Core.Logging.ILogger;

namespace UnitTests
{
    public class WarehouseFacadeTest
    {
        private Mock<ILogger<DataOperator>> logger;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<DataOperator>>();
        }
        [Test]
        public void WarehouseFacade_Ctor_ShouldThrownExceptionForNullInputReader()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WarehouseFacade(null, new DataParser(), new DataOperator(logger.Object)));
        }

        [Test]
        public void WarehouseFacade_Ctor_ShouldThrownExceptionForNullDataParser()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WarehouseFacade(new InputReader(),null, new DataOperator(logger.Object)));
        }

        [Test]
        public void WarehouseFacade_Ctor_ShouldThrownExceptionForNullDataOperator()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WarehouseFacade(new InputReader(), new DataParser(), null));
        }
    }
}