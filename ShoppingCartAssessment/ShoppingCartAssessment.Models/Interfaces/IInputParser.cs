using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartAssessment.Models.Implementations;

namespace ShoppingCartAssessment.Models.Interfaces
{
    public interface IInputParser
    {
        List<string> SeparateBaskets(string toParse);

        List<ShoppingCartItem> GenerateCartItems(string itemList);
    }
}
