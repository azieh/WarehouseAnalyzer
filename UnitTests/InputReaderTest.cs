using System;
using System.IO;
using System.Linq;
using Common.Interfaces;
using NUnit.Framework;
using Services;

namespace UnitTests
{
    public class InputReaderTest
    {
        [SetUp]
        public void Setup()
        {
            _sut = new InputReader();
        }

        private IInputReader _sut;

        private string exampleInputWithNewLines = 
            @"# Material inventory initial state as of Jan 01 2018
# New materials
Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10
Maple Dovetail Drawerbox;COM-124047;WH-A,15
Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2
Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11
# Existing materials, restocked
Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WHB,5
Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3MCherry-10mm;WH-A,10|WH-B,1
Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10
MDF, CARB2, 1 1/8""; COM-101734;WH-C,8";

        [TestCase("# Material inventory initial state as of Jan 01 2018")]
        [TestCase("# New materials")]
        [TestCase("# Existing materials, restocked")]
        public void InputReader_ReadInputData_ShouldNotContainSpecificText(string input)
        {
            //Arrange
            var sr = new StringReader(exampleInputWithNewLines);
            Console.SetIn(sr);

            //Act
            var result = _sut.ReadInputData();

            //Assert
            Assert.IsFalse(result.Any(_ => _.Contains(input)));
        }
    }
}