using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartAssessment.Models.Implementations;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models;
using System.Collections.Generic;

namespace ShoppingCartAssessment.Test
{
    [TestClass]
    public class SalesTaxCalculatorTest
    {
        [TestMethod]
        public void CalculateTax_TaxExemptItem_ReturnsZeroTax()
        {
            //Arrange
            var expectedResult = 0;
            var testCalc = new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    });
            var testItem = new ShoppingCartItem("testItem", 12.3, 1, new List<CartItemEnums.CartItemTypes> { CartItemEnums.CartItemTypes.Book }, false);

            //Act
            var result = testCalc.CalculateTax(testItem);

            //Assert
            Assert.AreEqual(expectedResult,result);
        }
    }
}
