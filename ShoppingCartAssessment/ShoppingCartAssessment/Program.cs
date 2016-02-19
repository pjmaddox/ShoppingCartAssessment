using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Implementations;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models;


namespace ShoppingCartAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input your shopping baskets and items:\n");

            //Retrieve input from user
            var stringInput = "";
            var lineInput = "";
            while ((lineInput = Console.ReadLine()) != "")
            {
                stringInput += (lineInput + '\n');
            }


            //Create new cart factory and inject appropriate parser
            var listOfExemptions = new List<CartItemEnums.CartItemTypes> {
                CartItemEnums.CartItemTypes.Medical,
                CartItemEnums.CartItemTypes.Food,
                CartItemEnums.CartItemTypes.Book
            };
            var cartFactory = new ShoppingCartFactory(new SalesTaxCalculator(.1, listOfExemptions),new ImportTaxCalculator(.05), new BaseCartParser());

            //Create carts
            var listOfCarts = cartFactory.CreateCarts(stringInput);

            //Gather output from each cart
            var outputString = "";
            foreach (ShoppingCart currentCart in listOfCarts)
            {
                outputString += currentCart.OutputContents();
            }

            //Output result string
            Console.Write(outputString);
            Console.Write("\n\nPress enter to exit program.\n");
            Console.ReadLine();
        }
    }
}
