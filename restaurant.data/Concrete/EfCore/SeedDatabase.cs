using System.Linq;
using Microsoft.EntityFrameworkCore;
using restaurant.entity;
namespace restaurant.data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            // var context = new ShopContext();

            // if (context.Database.GetPendingMigrations().Count() == 0)
            // {
            //     if (context.Categories.Count() == 0)
            //     {
            //         context.Categories.AddRange(Categories);
            //     }

            //     if (context.Foods.Count() == 0)
            //     {
            //         context.Foods.AddRange(Foods);
            //         // context.AddRange(FoodCategories);
                   
            //     }
            //     context.SaveChanges();
            // }
            
        }

        private static Category[] Categories = {
            new Category(){Name="Snacks",Url="snacks"},
            new Category(){Name="Salads",Url="salads"},
            new Category(){Name="Soups",Url="soups"},
            new Category(){Name="National Dishes",Url="national-dishes"},
            new Category(){Name="Burgers And Sandwiches",Url="burgers-and-Sandwiches"},
            new Category(){Name="Pies And Pizzas",Url="pies-and-Pizzas"},
            new Category(){Name="Drinks",Url="drinks"}

        };

        
         private static Food[] Foods = {
           new Food(){Name="Assorted sausages",Url="assorted-sausages",Description="Servalat, oxota, Indian ham, doctor",Price=12,ImageUrl="10.png"},
            new Food(){Name="Mushrooms",Url="Mushrooms",Description="Mushrooms in the oven",Price=22,ImageUrl="11.png"},
            new Food(){Name="Assorted fish",Url="assorted-fish",Description="Sturgeon, whitefish, salmon",Price=40,ImageUrl="12.png"},
            new Food(){Name="Caesar salad with chicken",Url="Caesar-salad-with-chicken",Description="Caesar sauce, parmesan cheese, iceberg, lettuce, toast bread, tomato cherry, chicken fillet, salt, vegetable oil",Price=25,ImageUrl="13.png"},
            new Food(){Name="Mimosa salad",Url="Mimosa-salad",Description="Potatoes, carrots, chicken fillet, mayonnaise, eggs, Dutch cheese",Price=22,ImageUrl="14.png"},
            new Food(){Name="Summer salad",Url="Summer-salad",Description="Tomatoes, cucumbers, greens, lettuce, carrots, red cabbage, olive oil, vinegar",Price=8,ImageUrl="15.png"},
            new Food(){Name="Chicken soup",Url="Chicken-soup",Description="Potatoes, Chicken, Rice",Price=10,ImageUrl="16.jpg"},
            new Food(){Name="Red lentil soup",Url="Red-lentil-soup",Description="Onion, Potato, Carrot, Butter",Price=12,ImageUrl="17.jpg"},
            new Food(){Name="Dovğa",Url="Dovga",Description="Eggs, Dill, Coriander, Flour, Mint, Yogurt, Rice Rice",Price=10,ImageUrl="18.jpg"}
        };
        // private static FoodCategory[] FoodCategories={
        //     new FoodCategory(){FoodId=Foods[0].FoodId,CategoryId=1},
        //     new FoodCategory(){FoodId=Foods[1].FoodId,CategoryId=1},
        //     new FoodCategory(){FoodId=Foods[2].FoodId,CategoryId=1},
        //     new FoodCategory(){FoodId=Foods[3].FoodId,CategoryId=2},
        //     new FoodCategory(){FoodId=Foods[5].FoodId,CategoryId=2},
        //     new FoodCategory(){FoodId=Foods[6].FoodId,CategoryId=2},
        //     new FoodCategory(){FoodId=Foods[7].FoodId,CategoryId=3},
        //     new FoodCategory(){FoodId=Foods[8].FoodId,CategoryId=3}
        //     //new FoodCategory(){FoodId=Foods[9].FoodId,CategoryId=Categories[2].CategoryId}
        // };
    }
}







