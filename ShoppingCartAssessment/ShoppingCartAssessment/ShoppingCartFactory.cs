using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Interfaces;
using ShoppingCartAssessment.Models.Implementations;
using ShoppingCartAssessment.Models;

namespace ShoppingCartAssessment
{
    public class ShoppingCartFactory
    {
        //Properties
        public ITaxCalculator ImportCalculator {get; set;}
        public ITaxCalculator SalesCalculator {get; set;}
        public IInputParser Parser {get; set;}

        //Constructors
        public ShoppingCartFactory(ITaxCalculator newImportCalc,ITaxCalculator newSalesCalc,IInputParser newParser)
        {
            this.ImportCalculator = newImportCalc;
            this.SalesCalculator = newSalesCalc;
            this.Parser = newParser;
        }

        //Methods
        public List<ShoppingCart> CreateCarts(string stringInput)
        {
            var resultList = new List<ShoppingCart>();
            int inputbasketCount = 1;

            var listOfBaskets = Parser.SeparateBaskets(stringInput);
            
            foreach (string currentString in listOfBaskets)
            {
                //Create new shopping cart and assign known properties
                var newShoppingCart = new ShoppingCart();
                newShoppingCart.TaxCalculators = new List<ITaxCalculator> { ImportCalculator, SalesCalculator };
                newShoppingCart.CartNumber = inputbasketCount;
                
                //Generate and assign cart items
                var itemsToAdd = Parser.GenerateCartItems(currentString);
                newShoppingCart.Items = itemsToAdd;
                resultList.Add(newShoppingCart);

                //Increment basket count
                ++inputbasketCount;
            }
            return resultList;
        }
    }
}
