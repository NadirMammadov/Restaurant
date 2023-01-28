using Microsoft.AspNetCore.Mvc;
using restaurant.webui.Models;
using System.Diagnostics;
using restaurant.business.Abstract;
using restaurant.data.Abstract;
using Microsoft.AspNetCore.Identity;
using restaurant.webui.Identity;

namespace restaurant.webui.Controllers
{
    public class FoodController : Controller
    {
        
        private IFoodService _foodService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;
        public FoodController(IFoodService foodService,UserManager<User> userManager, SignInManager<User> signManager)
        {
           _userManager = userManager;
            _foodService = foodService;
            _signManager = signManager;
        }
        
        public async Task<IActionResult> Index(string category,string tablenumber)
        { 
            if(tablenumber!=null)
            {
                var user = await _userManager.FindByNameAsync(tablenumber);
                var password ="Xezer2018";
                await _signManager.PasswordSignInAsync(user,password,false,false);
            }
            
            var foods= _foodService.GetFoodsByCategory(category);
            if(category!=null)
            {
                category=category.Replace('-',' ');
            }else{
                category=" ";
            }
            if(tablenumber==null)
            {
                tablenumber="empty";
            }
            var foodViewModel = new FoodViewModel()
            {
                Foods = foods,
                TableNumber = tablenumber,
                Category=category
            };
            
            return View(foodViewModel);
        }

        public async Task<IActionResult> FoodDetail(string foodName)
        {
            var food = _foodService.GetByName(foodName);
            var user= await _userManager.GetUserAsync(User);
            var username =" ";
            if(user!=null)
            {
                username= user.UserName.ToLower();
            }
           
            var foodmodel = new FoodModel()
            {
                FoodId= food.FoodId,
                Name =food.Name,
                Description= food.Description,
                Price=food.Price,
                ImageUrl=food.ImageUrl,
                UserName =username,
                Url=food.Url
            };

            return View(foodmodel);
        }
    }
}