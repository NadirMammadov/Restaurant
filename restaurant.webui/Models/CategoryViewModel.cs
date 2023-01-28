using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.webui.Models
{
    public class CategoryViewModel
    {
        public List<Category> Category { get; set; }
        public string CategoryName { get; set; }
    }
}