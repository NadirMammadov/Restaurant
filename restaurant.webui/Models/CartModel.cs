using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.webui.Models
{
    public class CartModel
    {
        public string UserId { get; set; } = null!;
        public int CartId { get; set; }
        public string UserName { get; set; } = null!;
        public List<CartItemModel> CartItems { get; set; } = null!;

        public double TotalPrice()
        {
            return CartItems.Sum(i=>i.Price*i.Quantity);
        }
    }
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double  Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}