using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurant.business.Abstract;
using restaurant.entity;
using restaurant.webui.Identity;
using restaurant.webui.Models;

namespace restaurant.webui.Controllers
{       
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;
        private IOrderService _orderService;

        public CartController(ICartService cartService, UserManager<User> userManager, SignInManager<User> signInManager, IOrderService orderService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _signManager = signInManager;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            var userId = _userManager.GetUserId(User);
            
            var user = await _userManager.GetUserAsync(User);
            var username = " ";
            if(user!=null)
            {
                username = user.UserName.ToLower();
            }
            return View(new CartModel()
            {
                CartId = cart.Id,
                UserId = userId,
                UserName = username,
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
        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodId, int quantity)
        {

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            user.NewOrder = true;
            await _userManager.UpdateAsync(user);
            _cartService.AddToCart(userId, foodId, quantity);
            return Redirect("/cart/item");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int foodId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, foodId);
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    FoodId = i.FoodId,
                    Name = i.Food.Name,
                    Price = (double)i.Food.Price,
                    ImageUrl = i.Food.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            return View(orderModel);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel model)
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCartByUserId(userId);

            model.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    FoodId = i.FoodId,
                    Name = i.Food.Name,
                    Price = (double)i.Food.Price,
                    ImageUrl = i.Food.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };

            var payment = PaymentProcess(model);

            if (payment.Status == "success")
            {
                SaveOrder(model, payment, userId);
                ClearCart(model.CartModel.CartId);
                await _signManager.SignOutAsync();
                return View("Success");
            }
            else
            {
                Console.WriteLine(payment.ErrorMessage);
            }
            return View(model);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);

        }

        private void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate =  new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.UserId = userId;
            order.Address = model.Address;
            order.Phone = model.Phone;
            order.Email = model.Email;
            order.Note = model.Note;
            order.City = model.City;
            order.OrderItems = new List<entity.OrderItem>();
            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new restaurant.entity.OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    FoodId = item.FoodId
                };

                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-i4R6Ab9NdlNfomGNBLYqtxXD2JkXSUib";
            options.SecretKey = "sandbox-4w8tji2FtSOLNG7aQsG99cIA7qidIRjK";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111, 999999).ToString();
            request.Price = model.CartModel.TotalPrice().ToString();
            request.PaidPrice = model.CartModel.TotalPrice().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;


            //  paymentCard.CardNumber = "5528790000000008";
            // paymentCard.ExpireMonth = "12";
            // paymentCard.ExpireYear = "2030";

            Buyer buyer = new Buyer();
            buyer.Id = "NOT FOUND";
            buyer.Name = model.FirstName;
            buyer.Surname = model.LastName;
            buyer.GsmNumber = "+905350000000";
            buyer.Email = model.Email;
            buyer.IdentityNumber = "Identity number 000";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Seni ilgilendirmez";
            buyer.Ip = "buda seni ilgilendirmez ip";
            buyer.City = model.City;
            buyer.Country = "Azerbaijan";
            buyer.ZipCode = "0000";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;
            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.FoodId.ToString();
                basketItem.Name = item.Name;
                basketItem.Price = (item.Price * item.Quantity).ToString();
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Category1 = "Food";
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);

        }
    }
}