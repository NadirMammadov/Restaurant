using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.business.Abstract;
using restaurant.data.Abstract;
using restaurant.data.Concrete.EfCore;
using restaurant.entity;

namespace restaurant.business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCart(string userId, int foodId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if(cart!=null)
            {
                var index = cart.CartItems.FindIndex(i=>i.FoodId==foodId);
                if(index<0)
                {
                    cart.CartItems.Add(new CartItem(){
                        FoodId= foodId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }else{
                    cart.CartItems[index].Quantity+=quantity;
                }
                _unitOfWork.Carts.Update(cart);
                _unitOfWork.Save();
            }
        }

        public void ClearCart(int cartId)
        {
            _unitOfWork.Carts.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId,int foodId)
        {
            var cart = GetCartByUserId(userId);
            if(cart!=null)
            {
                _unitOfWork.Carts.DeleteFromCart(cart.Id,foodId);
                _unitOfWork.Save();
            }
        }

        public List<Cart> GetAll()
        {
            
                return _unitOfWork.Carts.GetAll();
          
        }

        public Cart GetCartByUserId(string userId)
        {
            return _unitOfWork.Carts.GetByUserId(userId);
                
        }

        public void InitializeCart(string userId)
        {
            _unitOfWork.Carts.Create(new Cart(){
                UserId = userId
            });
            _unitOfWork.Save();
        }
    }
}