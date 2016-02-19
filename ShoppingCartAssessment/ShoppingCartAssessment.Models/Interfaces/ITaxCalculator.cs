using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Implementations;

namespace ShoppingCartAssessment.Models.Interfaces
{
    public interface ITaxCalculator
    {
        //Methods
        double CalculateTax(ShoppingCartItem item);
    }
}
