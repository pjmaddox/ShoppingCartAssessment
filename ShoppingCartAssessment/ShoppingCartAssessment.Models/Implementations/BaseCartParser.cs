using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Interfaces;

namespace ShoppingCartAssessment.Models.Implementations
{
    public class BaseCartParser : IInputParser
    {
        public List<string> SeparateBaskets(string input)
        {
            var resultList = new List<string>();

            var tempList = input.Split('\n');
            var currentCartString = "";
            bool isFirstBasket = true;

            //For each line determine if it should be added
            foreach(string currentLine in tempList)
            {
                //Check to see if this is a new shopping basket
                if (currentLine.Contains("Shopping Basket") || currentLine.Equals(tempList.Last()))
                {
                    if (!isFirstBasket)
                    {
                        resultList.Add(currentCartString);
                        currentCartString = "";
                    }
                    else
                    {
                        isFirstBasket = false;
                    }
                }
                else
                {
                    currentCartString += currentLine;
                    if (!currentLine.Equals(tempList.Last()))
                        currentCartString += '\n';
                }
            }
            return resultList;
        }

        public List<ShoppingCartItem> GenerateCartItems(string itemList)
        {
            var resultList = new List<ShoppingCartItem>();

            //Separate items into individual lines
            var itemLineList = itemList.Split('\n');

            foreach(string currentItem in itemLineList)
            {
                //Check for empty string. Indicates end of list or error
                if (currentItem.Equals(string.Empty))
                    break;

                var lastIndex = 0;
                var indexOfNextSeparation = 0;
                var newCartItem = new ShoppingCartItem();
                newCartItem.ItemTypes = new List<CartItemEnums.CartItemTypes>();

                //Get quantity
                indexOfNextSeparation = currentItem.IndexOf(' ');
                var quantitySubstring = currentItem.Substring(lastIndex, indexOfNextSeparation);
                newCartItem.Quantity = Convert.ToInt32(quantitySubstring);
                lastIndex = indexOfNextSeparation;

                //Get import status and name 
                indexOfNextSeparation = currentItem.IndexOf(' ',lastIndex+1);
                var importOrName = currentItem.Substring(lastIndex,indexOfNextSeparation - lastIndex);
                if (importOrName.Contains("imported"))
                {
                    //have import status
                    newCartItem.IsImported = true;

                    //Get Name
                    lastIndex = indexOfNextSeparation;
                    indexOfNextSeparation = currentItem.IndexOf("at ",lastIndex+1);
                    importOrName = (currentItem.Substring(lastIndex, indexOfNextSeparation - lastIndex)).Trim();

                    newCartItem.ItemName = importOrName;
                }
                else
                {
                    newCartItem.IsImported = false;

                    indexOfNextSeparation = currentItem.IndexOf("at ",lastIndex+1);
                    importOrName = currentItem.Substring(lastIndex, indexOfNextSeparation - lastIndex).Trim();

                    newCartItem.ItemName = importOrName;
                }

                lastIndex = indexOfNextSeparation;

                //assign types
                newCartItem = AssignTypesHelper(newCartItem);

                //Get price
                var priceAsString = currentItem.Substring(currentItem.IndexOf("at ",lastIndex)+3);
                newCartItem.ItemPrice = Convert.ToDouble(priceAsString);

                //Add resulting cart item to the list for this cart
                resultList.Add(newCartItem);
            }
            return resultList;
        }

        private ShoppingCartItem AssignTypesHelper(ShoppingCartItem item)
        {
            var newCartItem = item;

            if (newCartItem.ItemName.Contains("book"))
            {
                newCartItem.ItemTypes.Add(CartItemEnums.CartItemTypes.Book);
            }
            if (newCartItem.ItemName.Contains("chocolate"))
            {
                newCartItem.ItemTypes.Add(CartItemEnums.CartItemTypes.Food);
            }
            if (newCartItem.ItemName.Contains("pills"))
            {
                newCartItem.ItemTypes.Add(CartItemEnums.CartItemTypes.Medical);
            }

            return newCartItem;
        }
    }
}
