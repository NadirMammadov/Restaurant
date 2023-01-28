using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.entity
{
    
    public class FoodCategory
    {
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}