using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.entity
{
    public class Category
    {
       
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Food> Foods { get; set; }
        public List<FoodCategory> FoodCategories { get; set; }
        
    }
}