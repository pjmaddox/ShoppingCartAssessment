using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartAssessment.Models.Implementations;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models;

namespace ShoppingCartAssessment.Test
{
    [TestClass]
    public class ShoppingCartTest
    {
        [TestMethod]
        public void OutputContents_EmptyCartNoName_ReturnsEmptyString()
        {
            //Arrange
            var expectedResult = "Output 0:\nSales Tax: 0\nTotal: 0\n";
            var testCart = new ShoppingCart
            {
                CartNumber = 0,
                TaxCalculators = new List<ITaxCalculator> {
                    new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    }),
                    new ImportTaxCalculator(.05)
                },
                Items = new List<ShoppingCartItem> { }
            };

            //Act
            var actualResult = testCart.OutputContents();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OutputContents_OneTaxExemptItem_ReturnsFormattedCartAndItem()
        {
            //Arrange
            var expectedResult = "Output 1:\n1 book: 12.3\nSales Tax: 0\nTotal: 12.3\n";
            var testCart = new ShoppingCart { 
                CartNumber = 1,
                TaxCalculators = new List<ITaxCalculator> {
                    new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    }),
                    new ImportTaxCalculator(.05)
                },
                Items = new List<ShoppingCartItem> { new ShoppingCartItem("book",12.3,1,new List<CartItemEnums.CartItemTypes>{ CartItemEnums.CartItemTypes.Book },false)}
            };

            //Act
            var actualResult = testCart.OutputContents();

            //Assert
            Assert.AreEqual(expectedResult,actualResult);
        }

        [TestMethod]
        public void OutputContents_OneItem_ReturnsFormattedCartAndTaxedItem()
        {
            //Arrange
            var expectedResult = "Output 1:\n1 testItem: 13.55\nSales Tax: 1.25\nTotal: 13.55\n";
            var testCart = new ShoppingCart
            {
                CartNumber = 1,
                TaxCalculators = new List<ITaxCalculator> {
                    new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    }),
                    new ImportTaxCalculator(.05)
                },
                Items = new List<ShoppingCartItem> { new ShoppingCartItem("testItem", 12.3, 1, new List<CartItemEnums.CartItemTypes> { }, false) }
            };

            //Act
            var actualResult = testCart.OutputContents();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void OutputContents_OneImportedAndTaxedItem_ReturnsFormattedCartAndTaxedItem()
        {
            //Arrange
            var expectedResult = "Output 1:\n1 imported testItem: 14.2\nSales Tax: 1.9\nTotal: 14.2\n";
            var testCart = new ShoppingCart
            {
                CartNumber = 1,
                TaxCalculators = new List<ITaxCalculator> {
                    new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    }),
                    new ImportTaxCalculator(.05)
                },
                Items = new List<ShoppingCartItem> { new ShoppingCartItem("testItem", 12.3, 1, new List<CartItemEnums.CartItemTypes> {  }, true) }
            };

            //Act
            var actualResult = testCart.OutputContents();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void OutputContents_TwoTaxedItems_ReturnsFormattedCartAndTaxedItem()
        {
            //Arrange
            var expectedResult = "Output 1:\n1 testItem: 13.55\n2 imported testItem2: 10.49\nSales Tax: 1.75\nTotal: 24.04\n";
            var testCart = new ShoppingCart
            {
                CartNumber = 1,
                TaxCalculators = new List<ITaxCalculator> {
                    new SalesTaxCalculator(.1, new List<CartItemEnums.CartItemTypes> {
                        CartItemEnums.CartItemTypes.Book,
                        CartItemEnums.CartItemTypes.Food,
                        CartItemEnums.CartItemTypes.Medical
                    }),
                    new ImportTaxCalculator(.05)
                },
                Items = new List<ShoppingCartItem> { 
                    new ShoppingCartItem("testItem", 12.3, 1, new List<CartItemEnums.CartItemTypes> {  }, false),
                    new ShoppingCartItem("testItem2", 9.99, 2, new List<CartItemEnums.CartItemTypes> { CartItemEnums.CartItemTypes.Medical }, true)    
                }
            };

            //Act
            var actualResult = testCart.OutputContents();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
