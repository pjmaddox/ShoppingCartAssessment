using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Interfaces;

namespace ShoppingCartAssessment.Models.Implementations
{
    public class SalesTaxCalculator : ITaxCalculator
    {
        //Properties
        public List<CartItemEnums.CartItemTypes> ExemptTypes { get; set; }
        private double TaxRateDecimal { get; set; }

        //Constructors
        public SalesTaxCalculator() { }

        public SalesTaxCalculator(double newTaxRateDecimal, List<CartItemEnums.CartItemTypes> newExemptions)
        {
            this.TaxRateDecimal = newTaxRateDecimal;
            this.ExemptTypes = newExemptions;
        }

        //Methods
        /// <summary>
        /// Calculates and returns tax based on the input and this calculator's taxRateDecimal. Rounds up to the nearest 5 cents.
        /// </summary>
        /// <param name="itemCost"></param>
        /// <returns></returns>
        public double CalculateTax(ShoppingCartItem item)
        {
            var result = 0.0;
            var itemCost = item.ItemPrice;

            if (itemCost > 0 && !IsItemExempt(item))
            {
                //Store un-rounded tax rate
                var taxAmount = itemCost * TaxRateDecimal;

                //Find modulus of 5 cent interval (.05)
                var modFiveCents = taxAmount % (.05);

                //If the modulo is not zero, add the difference (equivalent to rounding up to the nearest 5-cent interval)
                if (modFiveCents != 0)
                {
                    taxAmount += (.05 - modFiveCents);
                }

                //Set result equal to the newly rounded taxAmount
                result = taxAmount;
            }

            return result;
        }

        //Helper Method
        private bool IsItemExempt(ShoppingCartItem toCheck)
        {
            bool hasMatch = toCheck.ItemTypes.Select(x => x)
                                             .Intersect(ExemptTypes)
                                             .Any();
            return hasMatch;
        }
    }
}
