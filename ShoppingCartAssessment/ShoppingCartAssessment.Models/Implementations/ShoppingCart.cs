using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Interfaces;

namespace ShoppingCartAssessment.Models.Implementations
{
    public class ShoppingCart
    {
        //Properties
        public IEnumerable<ShoppingCartItem> Items { get; set; }
        public int CartNumber { get; set; }
        public List<ITaxCalculator> TaxCalculators {get; set;}
        
        //Constructors
        public ShoppingCart() { }

        public ShoppingCart(int newNumber, IEnumerable<ShoppingCartItem> newItemsList, List<ITaxCalculator> newCalcList)
        {
            this.CartNumber = newNumber;
            this.Items = newItemsList;
            this.TaxCalculators = newCalcList;
        }

        //Methods
        public string OutputContents()
        {
            var runningTax = 0.0;
            var runningTotal = 0.0;
            var result = "";

            result += "Output " + CartNumber + ":\n";

            foreach (ShoppingCartItem item in Items)
            {
                var currentItemTax = GetTotalTaxForItem(item);
                runningTax += currentItemTax;
                runningTotal += (currentItemTax + item.ItemPrice);

                result += formatLine(item, currentItemTax);
            }

            //Report totals
            result += "Sales Tax: " + runningTax + '\n';
            result += "Total: " + runningTotal + '\n';

            return result;
        }

        //Private helper methods
        private double GetTotalTaxForItem(ShoppingCartItem item)
        {
            var runningTaxTotal = 0.0;

            foreach (ITaxCalculator currentCalc in TaxCalculators)
            {
                runningTaxTotal += currentCalc.CalculateTax(item);
            }

            return runningTaxTotal;
        }

        private string formatLine(ShoppingCartItem item, double totalItemTax)
        {
            var result = "";

            result += item.Quantity;

            //Check for imported, append text if so
            if (item.IsImported)
                result += " imported";

            result += " " + item.ItemName;
            result += ": " + (item.ItemPrice + totalItemTax);
            result += '\n';

            return result;
        }
    }
}
