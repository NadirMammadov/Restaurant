using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.webui.Models
{
    public class FoodViewModel
    {
        public List<Food> Foods { get; set; }
        public string  Category { get; set; }
        public string TableNumber { get; set; }
    }
}