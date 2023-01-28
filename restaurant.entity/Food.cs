using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.entity
{
    public class Food{
    
        
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string  Url { get; set; }
        public List<Category>  Categories { get; set; }
        public List<FoodCategory> FoodCategories { get; set; }
    }
}