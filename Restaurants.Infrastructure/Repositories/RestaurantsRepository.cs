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
		public async Task<IEnumerable<Restaurant>> GetAllByOwnerAsync(string ownerId)
		{
			var restaurants = await dbContext.Restaurants.Where(r => r.OwnerId == ownerId).ToListAsync();
			return restaurants;
		}
		public async Task<IEnumerable<Restaurant>> GetAllAsync()
		{
			var restaurants = await dbContext.Restaurants.ToListAsync();
			return restaurants;
		}

		public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
		{
			var searchPhraseLower = searchPhrase?.ToLower();

			var baseQuery = dbContext
				.Restaurants
				.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
														|| r.Description.ToLower().Contains(searchPhraseLower)));

			var totalCount = await baseQuery.CountAsync();

			var restaurants = await baseQuery
				.Skip(pageSize * (pageNumber) - 1)
				.Take(pageSize)
				.ToListAsync();

			return (restaurants, totalCount);
		}

		public async Task<int> Create(Restaurant entity)
		{
			dbContext.Restaurants.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<Restaurant?> FindByIdAsync(int id)
		{
			var restaurant = await dbContext.Restaurants
				.Include(r => r.Dishes)
				.FirstOrDefaultAsync(x => x.Id == id);
			return restaurant;
		}

		public async Task Delete(Restaurant entity)
		{
			dbContext.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task Update(Restaurant entity)
		{
			dbContext.Update(entity);
			await dbContext.SaveChangesAsync();
		}
	}
}
