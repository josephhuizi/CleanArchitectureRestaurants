using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
	internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
	{
		public async Task<IEnumerable<Restaurant>> GetAllAsync()
		{
			var restaurants = await dbContext.Restaurants.ToListAsync();
			return restaurants;
		}

		public async Task<int> Create(Restaurant entity)
		{
			dbContext.Restaurants.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		async Task<Restaurant?> IRestaurantsRepository.FindByIdAsync(int id)
		{
			var restaurant = await dbContext.Restaurants
				.Include(r => r.Dishes)
				.FirstOrDefaultAsync(x => x.Id == id);
			return restaurant;
		}
	}
}
