using System;
using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services;

namespace UnitTests
{
    public class DataOperatorTest
    {
        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<DataOperator>>();
            _sut = new DataOperator(_loggerMock.Object);
            _helper = new DataParser();
        }

        private IDataOperator _sut;
        private IDataParser _helper;
        private Mock<ILogger<DataOperator>> _loggerMock;

        private List<string> inputList =>
            new List<string>
        {
            "Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10",
            "Maple Dovetail Drawerbox;COM-124047;WH-A,15",
            "Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2",
            "Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11",
            "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WH-B,5",
            "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3MCherry-10mm;WH-A,10|WH-B,1",
            "Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10",
            @"MDF,CARB2, 1 1/8"";COM-101734;WH-C,8"
        };

        [Test]
        public void Operator_MapProductInfoToWarehouseProductInfoPerspective_ShouldHave14ElementsList()
        {
            //Arrange 
            var inputData = _helper.DeserializeToCompleteList(inputList);
            //Act
            var result = _sut.MapProductInfoToWarehouseProductInfoPerspective(inputData);

            //Assert
            Assert.AreEqual(14, result.Count);
        }

        [Test]
        public void Operator_Ctor_ShouldThrownExceptionForNullLogger()
        {
            Assert.Throws<ArgumentNullException>(() => new DataOperator(null));
        }

        [Test]
        public void Operator_MapProductInfoToWarehousePerspective_ShouldHave4ElementsList()
        {
            //Arrange 
            var inputData = _helper.DeserializeToCompleteList(inputList);
            var productPerspectiveList = _sut.MapProductInfoToWarehouseProductInfoPerspective(inputData);
            //Act
            var result = _sut.MapProductInfoToWarehousePerspective(productPerspectiveList);

            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void Operator_MapProductInfoToWarehousePerspective_ShouldRespectAlphabeticSortWhenTotalIsSame()
        {
            //Arrange 
            var data =
                new List<string>
                {
                    "Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10",
                    "Maple Dovetail Drawerbox;COM-124047;WH-A,5|WH-B,10",
                    "Generic Wire Pull;COM-123906c;WH-C,20"
                };
            var inputData = _helper.DeserializeToCompleteList(data);
            var productPerspectiveList = _sut.MapProductInfoToWarehouseProductInfoPerspective(inputData);
            //Act
            var result = _sut.MapProductInfoToWarehousePerspective(productPerspectiveList);

            //Assert
            Assert.AreEqual("WH-C", result[0].WarehouseName);
            Assert.AreEqual("WH-B", result[1].WarehouseName);
            Assert.AreEqual("WH-A", result[2].WarehouseName);
        }
    }
}