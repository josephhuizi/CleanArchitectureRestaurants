using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    await dbContext.Restaurants.AddRangeAsync(restaurants);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User),
                    new (UserRoles.Owner),
                    new (UserRoles.Admin)
                ];

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants =  
            [
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (Kentucky Fried Chicken) is an American fast food restaurant chain specializing in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = [
                        new()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30m,
                        },
                        new()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (10 pcs.)",
                            Price = 5.30m,
                        }
                    ],
                    Address = new()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast Food",
                    Description = "McDonald's is an American fast food restaurant chain specializing in burgers, fries, and other fast food items.",
                    ContactEmail = "contact@mcdonalds.com",
                    HasDelivery = true,
                    Address = new()
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "WIF 8SR"
                    }
                }
            ];

            return restaurants;
        }
    }
}



