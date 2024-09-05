using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
	internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
	{
		public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
		{
			logger.LogInformation("Getting all restaurants");
			var restaurants = await restaurantsRepository.GetAllAsync();
			return restaurants;
		}

		async Task<Restaurant?> IRestaurantsService.GetRestaurantById(int id)
		{
			logger.LogInformation($"Getting restaurant with ID {id}");
			var restaurant = await restaurantsRepository.FindByIdAsync(id);
			return restaurant;
		}
	}
}
