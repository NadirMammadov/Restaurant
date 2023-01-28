using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.webui.Models
{
    public class FoodModel
    {
        
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public List<Category> SelectedCategories { get; set; }
         public List<Category> Categories { get; set; }
    }
}