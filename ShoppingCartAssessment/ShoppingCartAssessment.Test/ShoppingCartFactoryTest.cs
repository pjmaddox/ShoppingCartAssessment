using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartAssessment.Models.Implementations;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment;
using ShoppingCartAssessment.Models;
using System.Collections.Generic;

namespace ShoppingCartAssessment.Test
{
    [TestClass]
    public class ShoppingCartFactoryTest
    {
        [TestMethod]
        public void GenerateShoppingCart_NoItems_ReturnsEmptyCart()
        {
            //Arrange
            var listOfExemptions = new List<CartItemEnums.CartItemTypes> {
                CartItemEnums.CartItemTypes.Medical,
                CartItemEnums.CartItemTypes.Food,
                CartItemEnums.CartItemTypes.Book
            };
            var testFactory = new ShoppingCartFactory(new SalesTaxCalculator(.1, listOfExemptions), new ImportTaxCalculator(.05), new BaseCartParser());

            var testInput = "";
            ShoppingCart[] expectedOutput = { };

            //Act
            var result = testFactory.CreateCarts(testInput);

            //Assert
            var isEqual = true;
            var counter = 0;
            foreach (ShoppingCart currentCart in result)
            {
                if (!areEqual(currentCart,expectedOutput[counter]))
                    isEqual = false;
                ++counter;
            }

            Assert.IsTrue(isEqual);
        }
        //helper methods
        private bool areEqual(ShoppingCart toCheck, ShoppingCart expected)
        {
            var isEqual = false;

            if (toCheck.CartNumber == expected.CartNumber)
                isEqual = true;

            var expectedEnumerator = expected.Items.GetEnumerator();
            foreach (ShoppingCartItem item in toCheck.Items)
            {
                var hasNext = expectedEnumerator.MoveNext();
                var expectedItem = expectedEnumerator.Current;
                if (!hasNext || checkIfItemEqual(item,expectedItem))
                    isEqual = false;
            }

            return isEqual;
        }
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
