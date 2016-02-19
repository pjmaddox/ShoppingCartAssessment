using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAssessment.Models.Implementations
{
    public class ShoppingCartItem
    {
        //Properties
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }
        public List<CartItemEnums.CartItemTypes> ItemTypes { get; set; }
        public bool IsImported { get; set; }

        //Constructors
        public ShoppingCartItem() { }

        public ShoppingCartItem(string newName, double newPrice, int newQuantity, List<CartItemEnums.CartItemTypes> newTypes, bool newImportStatus) 
        {
            this.ItemName = newName;
            this.ItemPrice = newPrice;
            this.Quantity = newQuantity;
            this.ItemTypes = newTypes;
            this.IsImported = newImportStatus;
        }
    }
}
