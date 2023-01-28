using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId,int foodId, int quantity);
        void DeleteFromCart(string userId,int foodId);
        void ClearCart(int cartId);
        List<Cart> GetAll();
    }
}