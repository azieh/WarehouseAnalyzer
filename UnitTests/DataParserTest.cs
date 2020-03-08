using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Interfaces;
using NUnit.Framework;
using Services;

namespace UnitTests
{
    public class DataParserTest
    {
        [SetUp]
        public void Setup()
        {
            _sut = new DataParser();
        }

        private IDataParser _sut;


        [TestCase(@"Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10", "Cherry Hardwood Arched Door - PS", "COM-100001")]
        [TestCase(@"MDF, CARB2, 1 1/8""; COM-101734;WH-C,8", @"MDF, CARB2, 1 1/8""", "COM-101734")]
        [TestCase(@"Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11", @"Yankee Hardware 110 Deg. Hinge", "COM-123908")]
        public void DataParser_DeserializeToInputMaterial_ShouldReturnExpectedModel(string input, string name, string id)
        {
            //Arrange

            //Act
            var result = _sut.DeserializeToInputMaterial(input);

            //Assert
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(id, result.Id);
        }

        [TestCase(@"COM-100001;WH-A,5|WH-B,10")]
        [TestCase(@"MDF, CARB2, 1 1/8"";WH-C,8")]
        [TestCase(@"Yankee Hardware 110 Deg. Hinge;COM-123908;")]
        public void DataParser_DeserializeToInputMaterial_ShouldThrowException(string input)
        {
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.DeserializeToInputMaterial(input));
        }


        [Test]
        public void DataParser_DeserializeToWarehouseItems_ShouldReturnExpectedModel1()
        {
            //Arrange
            string input = @"Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11";

            //Act
            var result = _sut.DeserializeToWarehouseItems(input);

            //Assert
            var assert1 = result.FirstOrDefault(x => x.Name == "WH-A");
            var assert2 = result.FirstOrDefault(x => x.Name == "WH-B");
            Assert.NotNull(assert1);
            Assert.NotNull(assert2);
            Assert.AreEqual(10, assert1.Quantity);
            Assert.AreEqual(11, assert2.Quantity);
        }

        [TestCase(@"COM-100001;WH-A,5|WH-B,10")]
        [TestCase(@"MDF, CARB2, 1 1/8"";WH-C,8")]
        [TestCase(@"Yankee Hardware 110 Deg. Hinge;COM-123908;")]
        public void DataParser_DeserializeToWarehouseItems_ShouldThrowException(string input)
        {
            //Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.DeserializeToWarehouseItems(input));
        }

        [Test]
        public void DataParser_DeserializeToWarehouseItems_ShouldReturnExpectedModel2()
        {
            //Arrange
            string input = @"MDF, CARB2, 1 1/8""; COM-101734;WH-C,8";

            //Act
            var result = _sut.DeserializeToWarehouseItems(input);

            //Assert
            var assert1 = result.FirstOrDefault(x => x.Name == "WH-C");
            Assert.NotNull(assert1);
            Assert.AreEqual(8, assert1.Quantity);
        }

        [Test]
        public void DataParser_DeserializeCompleteList_ShouldReturnExpectedModel()
        {
            //Arrange
            var input = new List<string>
            {
                "Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10",
                "Maple Dovetail Drawerbox;COM-124047;WH-A,15",
                "Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2",
                "Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11",
                "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WHB,5",
                "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3MCherry-10mm;WH-A,10|WH-B,1",
                "Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10",
                @"MDF, CARB2, 1 1/8""; COM - 101734; WH - C,8"
            };

            //Act
            var result = _sut.DeserializeToCompleteList(input);

            //Assert
            var assert1 = result.FirstOrDefault(x => x.Name == "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black");
            var assert2 = result.FirstOrDefault(x => x.Name == "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back");
            var assert3 = result.FirstOrDefault(x => x.Name == "Maple Dovetail Drawerbox");
            Assert.NotNull(assert1);
            Assert.NotNull(assert2);
            Assert.NotNull(assert3);
            Assert.IsTrue(assert1.Warehouse.Any());
            Assert.IsTrue(assert2.Warehouse.Any());
            Assert.IsTrue(assert3.Warehouse.Any());
        }
    }
}