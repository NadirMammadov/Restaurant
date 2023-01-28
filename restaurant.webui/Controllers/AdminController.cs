using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurant.business.Abstract;
using restaurant.entity;
using restaurant.webui.Identity;
using restaurant.webui.Models;
using ZXing;
using ZXing.QrCode;

namespace restaurant.webui.Controllers
{
   
    public class AdminController : Controller
    {
        private IFoodService _foodService;
        private ICategoryService _categoryService;
        private ICartService _cartService;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private IOrderService _orderService;
        public AdminController(IWebHostEnvironment hostEnvironment,IFoodService foodservice, ICategoryService categoryService, UserManager<User> userManager, ICartService cartService, RoleManager<IdentityRole> roleManager,IOrderService orderService)
        {
            _foodService = foodservice;
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
            _cartService = cartService;
            _hostEnvironment = hostEnvironment;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult RoleCreate()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {

            var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
            if (result.Succeeded)
            {
                return RedirectToAction("RoleList");
            }
            else
            {
                foreach (var message in result.Errors)
                {
                    ModelState.AddModelError("",message.Description);
                }
                return View(model);
            }
            
        }
        [Authorize(Roles ="Admin")]
        public IActionResult FoodList()
        {
            return View(new FoodViewModel()
            {
                Foods = _foodService.GetAll()
            });
        }
         [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RoleEdit(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers = new List<User>();

            foreach (var user in _userManager.Users.ToList())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)
                                ? members : nonmembers;
                list.Add(user);
            }
            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NoMembers = nonmembers
            };
            return View(model);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {

            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                        }
                    }
                }
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                        }
                    }
                }
            }


            return Redirect("/admin/rolelist");
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleDelete(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("RoleList");
        }
         [Authorize(Roles ="Admin")]
        public IActionResult AddFood()
        {
            return View();
        }
         [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddFood(FoodModel model, IFormFile file)
        {
            var entity = new Food()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Url = model.Url
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomname = string.Format($"{Guid.NewGuid()}{extention}");
                entity.ImageUrl = randomname;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomname);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _foodService.Create(entity);
            return RedirectToAction("FoodList");
        }
         [Authorize(Roles ="Admin")]
        public IActionResult FoodEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _foodService.GetByIdWithCategories((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new FoodModel()
            {
                Name = entity.Name,
                FoodId = entity.FoodId,
                Url = entity.Url,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.FoodCategories.Select(i => i.Category).ToList(),
                Categories = _categoryService.GetAll()
            };

            return View(model);
        }
         [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> FoodEdit(FoodModel model, int[] categoryIds, IFormFile file)
        {
            var entity = _foodService.GetById(model.FoodId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Url = model.Url;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomname = string.Format($"{Guid.NewGuid()}{extention}");
                entity.ImageUrl = randomname;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomname);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _foodService.Update(entity, categoryIds);
            return RedirectToAction("FoodList");
        }
         [Authorize(Roles ="Admin")]
        public IActionResult FoodDelete(int foodId)
        {
            var entity = _foodService.GetById(foodId);
            if (entity != null)
            {
                _foodService.Delete(entity);
            }
            return RedirectToAction("FoodList");
        }
         [Authorize(Roles ="Admin")]
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
         [Authorize(Roles ="Admin")]
        public IActionResult AddCategory()
        {
            return View();
        }
         [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Url = model.Url
            };
            _categoryService.Create(entity);
            return RedirectToAction("CategoryList");
        }
         [Authorize(Roles ="Admin")]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetByIdWithFoods((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Foods = entity.FoodCategories.Select(p => p.Food).ToList()
            };
            return View(model);
        }
         [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.CategoryId);
            entity.Name = model.Name;
            entity.Url = model.Url;
            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");
        }
        
        public IActionResult CategoryDelete(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            _categoryService.Delete(entity);
            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int foodId, int categoryId)
        {
            _categoryService.DeleteFromCategory(foodId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }
        public IActionResult Orders()
        {
            return View(_orderService.GetAll());
        }
        public IActionResult OrderDetails(int id)
        {
           var orders= _orderService.GetOrderDetails(id);
            
            return View(orders);
        }
        [Authorize(Roles ="Admin,Waiter")]
        public IActionResult UserList()
        {
            Response.Headers.Add("Refresh","5");
            return View(_userManager.Users);
        }
        [Authorize(Roles ="Admin,Waiter")]
        public async Task<IActionResult> SeenCart(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.NewOrder=false;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("UserList");
        }
        
        public async Task<IActionResult> CallWaiter(string userId)
        {  
            var user =  await _userManager.FindByIdAsync(userId);
            user.Waiter = true;
            await _userManager.UpdateAsync(user);
            return Redirect("/cart/item");
        }
        public async Task<IActionResult> GoneWaiter(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Waiter =false;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("UserList");
        }
        [Authorize(Roles ="Waiter,Admin")]
        [HttpPost]
        public async Task<IActionResult> Odemek(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            var cart = _cartService.GetCartByUserId(userId);//bunu oyren 
            var newmail = new Random().Next(1111, 9999);
            user.UserName = newmail.ToString();
            var result = await _userManager.UpdateAsync(user);
            SaveOrder(userId);
            _cartService.ClearCart(cart.Id);
            
            return RedirectToAction("UserList");
        }
        private async void SaveOrder(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var cart = _cartService.GetCartByUserId(userId);
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentId = "0";
            order.ConversationId = "0";
            order.OrderDate = new DateTime();
            order.FirstName = user.FirstName;
            order.LastName = "";
            order.UserId = userId;
            order.Address = "";
            order.Phone = "";
            order.Email = user.Email;
            order.Note = "";
            order.City = "";
            order.OrderItems = new List<entity.OrderItem>();
            foreach (var item in cart.CartItems)
            {
                var orderItem = new restaurant.entity.OrderItem()
                {
                    Price = item.Food.Price,
                    Quantity = item.Quantity,
                    FoodId = item.FoodId
                };

                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }
        [Authorize(Roles ="Waiter,Admin")]
        public IActionResult UserOrderList(string userId)
        {
            var cart = _cartService.GetCartByUserId(userId);
            return View(new CartModel()
            {
                CartId = cart.Id,
                UserId = userId,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    Name = i.Food.Name,
                    Price = i.Food.Price,
                    ImageUrl = i.Food.ImageUrl,
                    Quantity = i.Quantity,
                    FoodId = i.FoodId

                }).ToList()
            });
        }
        public IActionResult QrCodeCreate()
        {
            return View(_userManager.Users);
        }
        [HttpPost]
         public IActionResult QrCodeCreate(IFormCollection formCollection)
        {
            var writer = new QRCodeWriter();
            var resultBit = writer.encode("https://localhost:7003/menu/table/"+formCollection["QRCodeString"], BarcodeFormat.QR_CODE, 150, 150);
            var matrix = resultBit;
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);
            for(int x= 0;x<matrix.Height;x++)
            {
                for(int y=0;y<matrix.Width;y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.Crimson;
                    for(int i=0; i<scale;i++)
                        for(int j=0; j<scale;j++)
                            result.SetPixel(x*scale+i,y*scale+j,pixel);
                }
            }
            string webRootPath = _hostEnvironment.WebRootPath;
            result.Save(webRootPath + "\\img\\QrcodeNew.png");
            ViewBag.URL = "\\img\\QrcodeNew.png";
        
            return View(_userManager.Users);
        }
        public IActionResult ReadQRCode()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var path = webRootPath + "\\img\\QrcodeNew.png";
            var reader = new BarcodeReaderGeneric();
            Bitmap image = (Bitmap)Image.FromFile(path);
            using(image)
            {
                LuminanceSource source;
                source = new ZXing.Windows.Compatibility.BitmapLuminanceSource(image);
                Result result = reader.Decode(source);
                ViewBag.Text = result.Text;
            }
            return View("QrCodeCreate");
        }
    }
}