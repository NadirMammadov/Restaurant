using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.webui.Models
{
    public class CategoryModel
    {
        public int  CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Food> Foods { get; set; }
    }
}