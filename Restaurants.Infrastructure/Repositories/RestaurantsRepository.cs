﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber,
			string? sortBy, SortDirection sortDirection)
		{
			var searchPhraseLower = searchPhrase?.ToLower();

			var baseQuery = dbContext
				.Restaurants
				.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
														|| r.Description.ToLower().Contains(searchPhraseLower)));

			var totalCount = await baseQuery.CountAsync();

			if (sortBy != null)
			{
				var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
				{
					{ nameof(Restaurant.Name), r => r.Name },
					{ nameof(Restaurant.Description), r => r.Description },
					{ nameof(Restaurant.Category), r => r.Category },
				};

				var selectedColumn = columnsSelector[sortBy];

				baseQuery = sortDirection == SortDirection.Ascending
					? baseQuery.OrderBy(selectedColumn)
					: baseQuery.OrderByDescending(selectedColumn);
			}

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
