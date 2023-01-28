using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restaurant.business.Abstract;
using restaurant.webui.Models;

namespace restaurant.webui.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke(string category){
            var categorymodel= new CategoryViewModel()
            {
                Category=_categoryService.GetAll(),
                CategoryName= category
            };
            return View(categorymodel);
        }
    }
}