using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.data.Abstract;
using restaurant.entity;

namespace restaurant.data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public EfCoreCartRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public void ClearCart(int cartId)
        {

            var cmd = @"delete from CartItems where CartId=@p0";
            ShopContext.Database.ExecuteSqlRaw(cmd, cartId);

        }

        public void DeleteFromCart(int cartId, int foodId)
        {

            var cmd = @"delete from CartItems where CartId=@p0 and FoodId=@p1";
            ShopContext.Database.ExecuteSqlRaw(cmd, cartId, foodId);

        }

        public Cart GetByUserId(string userId)
        {

            return ShopContext.Carts
                         .Include(i => i.CartItems)
                         .ThenInclude(i => i.Food)
                         .FirstOrDefault(i => i.UserId == userId);

        }
        public override void Update(Cart entity)
        {

            ShopContext.Carts.Update(entity);

        }
    }
}