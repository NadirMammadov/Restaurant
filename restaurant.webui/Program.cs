using restaurant.data.Concrete.EfCore;
using restaurant.business.Abstract;
using restaurant.business.Concrete;
using restaurant.data.Abstract;
using restaurant.webui.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection")));
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //reqem girmelidi 
    options.Password.RequireLowercase = true; //kicik herif
    options.Password.RequireUppercase = true; //boyuk herif
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".Restaurant.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };


});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFoodService, FoodManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
var app = builder.Build();
SeedDatabase.Seed();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
        name: "Checkout",
        pattern: "/checkout",
        defaults: new { controller = "Cart", action = "Checkout" }
    );
    endpoints.MapControllerRoute(
        name: "Cart",
        pattern: "cart/item",
        defaults: new { controller = "Cart", action = "Index" }
    );
    endpoints.MapControllerRoute(
        name: "rolecreate",
        pattern: "admin/role/create",
        defaults: new { controller = "Admin", action = "RoleCreate" }
    );
    endpoints.MapControllerRoute(
        name: "Order Details",
        pattern: "admin/order/detail/{id?}",
        defaults: new { controller = "Admin", action = "OrderDetails" }
    );
    endpoints.MapControllerRoute(
        name: "Orders",
        pattern: "admin/orders",
        defaults: new { controller = "Admin", action = "Orders" }
    );
    endpoints.MapControllerRoute(
        name: "qrcodecreate",
        pattern: "admin/qrcode",
        defaults: new { controller = "Admin", action = "QrCodeCreate" }
    );
    endpoints.MapControllerRoute(
        name: "rolelist",
        pattern: "admin/role",
        defaults: new { controller = "Admin", action = "RoleList" }
    );
    endpoints.MapControllerRoute(
        name: "roleedit",
        pattern: "admin/role/{id?}",
        defaults: new { controller = "Admin", action = "RoleEdit" }
    );


    endpoints.MapControllerRoute(
        name: "userlist",
        pattern: "admin/users",
        defaults: new { controller = "Admin", action = "UserList" }
    );
    endpoints.MapControllerRoute(
        name: "userlist",
        pattern: "admin/user/order/{userId}",
        defaults: new { controller = "Admin", action = "UserOrderList" }
    );
    endpoints.MapControllerRoute(
        name: "addFood",
        pattern: "admin/add/food",
        defaults: new { controller = "Admin", action = "AddFood" }
    );
    endpoints.MapControllerRoute(
        name: "foodlist",
        pattern: "admin/foods",
        defaults: new { controller = "Admin", action = "FoodList" }
    );
    endpoints.MapControllerRoute(
        name: "foodedit",
        pattern: "admin/foods/{id?}",
        defaults: new { controller = "Admin", action = "FoodEdit" }
    );
    endpoints.MapControllerRoute(
        name: "admincategories",
        pattern: "admin/categories",
        defaults: new { controller = "Admin", action = "CategoryList" }
    );
    endpoints.MapControllerRoute(
        name: "admincategories",
        pattern: "admin/add/category",
        defaults: new { controller = "Admin", action = "AddCategory" }
    );

    endpoints.MapControllerRoute(
        name: "admincategories",
        pattern: "admin/categories/{id?}",
        defaults: new { controller = "Admin", action = "CategoryEdit" }
    );
    endpoints.MapControllerRoute(
        name: "adminpanel",
        pattern: "admin/panel",
        defaults: new { controller = "Admin", action = "Index" }
    );
    endpoints.MapControllerRoute(
        name: "Food",
        pattern: "menu/food/detail/{foodName}",
        defaults: new { controller = "Food", action = "FoodDetail" }
    );
    endpoints.MapControllerRoute(
       name: "Food",
       pattern: "menu/table/{tablenumber?}",
       defaults: new { controller = "Food", action = "Index" }
   );
    endpoints.MapControllerRoute(
        name: "Food",
        pattern: "menu/{category?}/table/{tablenumber?}",
        defaults: new { controller = "Food", action = "Index" }
    );
    endpoints.MapControllerRoute(
        name: "Food",
        pattern: "menu/{category?}",
        defaults: new { controller = "Food", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
