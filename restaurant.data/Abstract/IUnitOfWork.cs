using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.data.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        IFoodRepository Foods {get;}
        ICartRepository Carts {get;}
        ICategoryRepository Categories {get;}
        IOrderRepository Orders {get;}
         void Save();
    }
}