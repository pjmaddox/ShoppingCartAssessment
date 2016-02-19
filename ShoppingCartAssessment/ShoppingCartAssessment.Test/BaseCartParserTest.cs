using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models.Implementations;
using System.Collections.Generic;
using ShoppingCartAssessment.Models;

namespace ShoppingCartAssessment.Test
{
    [TestClass]
    public class BaseCartParserTest
    {
        [TestMethod]
        public void SeparateBaskets_TwoEmptyCarts_ReturnsListOfTwoEmptyStrings()
        {
            //Arrange
            var input = "Shopping Basket 1:\nShopping Basket 2:\n";
            var testParser = new BaseCartParser();
            var expectedOutputForBoth = "";

            //Act
            var result = testParser.SeparateBaskets(input);

            var isEqual = true;
            var counter = 0;
            foreach (string currentString in result)
            {
                if (!currentString.Equals(expectedOutputForBoth))
                    isEqual = false;
                ++counter;
            }

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void SeparateBaskets_TwoCarts_ReturnsListOfTwoStrings()
        {
            //Arrange
            var input = "Shopping Basket 1:\n1 chocolate at 0.85\nShopping Basket 2:\n1 chocolate at 0.85\n";
            var testParser = new BaseCartParser();
            var expectedOutput = new List<string> { "1 chocolate at 0.85\n", "1 chocolate at 0.85\n" };

            //Act
            var result = testParser.SeparateBaskets(input);

            //Assert
            var expectedArray = expectedOutput.ToArray();

            var isEqual = true;
            var counter = 0;
            foreach (string currentString in result)
            {
                if (!currentString.Equals(expectedArray[counter]))
                    isEqual = false;
                ++counter;
            }

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void SeparateBaskets_TwoCartsTwoItems_ReturnsListOfTwoStrings()
        {
            //Arrange
            var input = "Shopping Basket 1:\n1 chocolate at 0.85\n2 toy at 1.2\nShopping Basket 2:\n1 chocolate at 0.85\n";
            var testParser = new BaseCartParser();
            var expectedOutput = new List<string> { "1 chocolate at 0.85\n2 toy at 1.2\n", "1 chocolate at 0.85\n" };

            //Act
            var result = testParser.SeparateBaskets(input);

            //Assert
            var expectedArray = expectedOutput.ToArray();

            var isEqual = true;
            var counter = 0;
            foreach (string currentString in result)
            {
                if (!currentString.Equals(expectedArray[counter]))
                    isEqual = false;
                ++counter;
            }

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void GernerateCartItems_EmptyCart_ReturnsEmptyList()
        {
            //Arrange
            var testParser = new BaseCartParser();
            var input = "";

            //Act
            var result = testParser.GenerateCartItems(input);

            //Assert
            var listIsEmpty = result.Count == 0;
            Assert.IsTrue(listIsEmpty);
        }
        [TestMethod]
        public void GernerateCartItems_OneItemFood_ReturnsListOfOne()
        {
            //Arrange
            var testParser = new BaseCartParser();
            var expectedResult =  new ShoppingCartItem {
                ItemName = "chocolate",
                ItemTypes = new List<CartItemEnums.CartItemTypes> { CartItemEnums.CartItemTypes.Food },
                IsImported = false,
                ItemPrice = 0.85,
                Quantity = 1
            };
            var input = "1 chocolate at 0.85\n";

            //Act
            var result = testParser.GenerateCartItems(input);
            
            //Assert
            var isEqual = true;
            foreach (ShoppingCartItem item in result)
            {
                if (!checkIfItemEqual(item, expectedResult))
                    isEqual = false;
            }
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void GernerateCartItems_ManyItems_ReturnsListOfMany()
        {
            //Arrange
            var testParser = new BaseCartParser();
            ShoppingCartItem[] expectedResult = { new ShoppingCartItem {
                ItemName = "chocolate",
                ItemTypes = new List<CartItemEnums.CartItemTypes> { CartItemEnums.CartItemTypes.Food },
                IsImported = false,
                ItemPrice = 0.85,
                Quantity = 1
            },
            new ShoppingCartItem {
                ItemName = "toy",
                ItemTypes = new List<CartItemEnums.CartItemTypes> { },
                IsImported = true,
                ItemPrice = 56.93,
                Quantity = 8
            },
            new ShoppingCartItem {
                ItemName = "headache pills",
                ItemTypes = new List<CartItemEnums.CartItemTypes> { CartItemEnums.CartItemTypes.Medical },
                IsImported = false,
                ItemPrice = 10.99,
                Quantity = 1
            }};
            var input = "1 chocolate at 0.85\n8 imported toy at 56.93\n1 headache pills at 10.99";

            //Act
            var result = testParser.GenerateCartItems(input);

            //Assert
            var isEqual = true;
            var counter = 0;
            foreach (ShoppingCartItem item in result)
            {
                if (!checkIfItemEqual(item, expectedResult[counter]))
                    isEqual = false;
                ++counter;
            }
            Assert.IsTrue(isEqual);
        }

        //helper method
        private bool checkIfItemEqual(ShoppingCartItem itemToCheck, ShoppingCartItem expected)
        {
            var result = false;
            if (itemToCheck.IsImported == expected.IsImported &&
                itemToCheck.ItemName.Equals(expected.ItemName) &&
                itemToCheck.ItemPrice == expected.ItemPrice &&
                itemToCheck.Quantity == expected.Quantity &&
                TypeListsEqual(itemToCheck.ItemTypes, expected.ItemTypes))
                result = true;
            return result;
        }
        private bool TypeListsEqual(List<CartItemEnums.CartItemTypes> toCheck, List<CartItemEnums.CartItemTypes> expected)
        {
            var result = true;

            foreach (CartItemEnums.CartItemTypes currentType in expected)
            {
                if (!toCheck.Contains(currentType))
                    result = false;
            }

            return result;
        }
    }
}
