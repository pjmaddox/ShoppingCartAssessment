using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models.Implementations;

namespace ShoppingCartAssessment.Test
{
    [TestClass]
    public class ImportTaxCalculatorTest
    {
        [TestMethod]
        public void CalculateTax_NotImported_ReturnsZero()
        {
            //Arrange
            var importTaxCalc = new ImportTaxCalculator(.05);
            var itemPrice = 39.70;

            //Act
            var result = importTaxCalc.CalculateTax(new ShoppingCartItem { ItemPrice = itemPrice, IsImported = false });

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateTax_PositiveValue_ReturnsRoundedTaxValue()
        {
            //Arrange
            var importTaxCalc = new ImportTaxCalculator(.05);
            var itemPrice = 39.70;

            //Act
            var result = importTaxCalc.CalculateTax(new ShoppingCartItem { ItemPrice = itemPrice, IsImported = true });

            //Assert
            Assert.AreEqual(2.0, result);
        }
        
        [TestMethod]
        public void CalculateTax_NegativeValue_ReturnsZero()
        {
            //Arrange
            var importTaxCalc = new ImportTaxCalculator(.05);
            var itemPrice = -10.3;

            //Act
            var result = importTaxCalc.CalculateTax(new ShoppingCartItem { ItemPrice = itemPrice, IsImported = true });

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateTax_ZeroValue_ReturnsZero()
        {
            //Arrange
            var importTaxCalc = new ImportTaxCalculator(.05);
            var itemPrice = 0;

            //Act
            var result = importTaxCalc.CalculateTax(new ShoppingCartItem { ItemPrice = itemPrice, IsImported = false });

            //Assert
            Assert.AreEqual(0, result);
        }
    }
}
