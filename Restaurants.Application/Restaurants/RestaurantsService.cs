using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
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
		public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurants()
		{
			logger.LogInformation("Getting all restaurants");
			var restaurants = await restaurantsRepository.GetAllAsync();

			var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
			return restaurantsDto;
		}

		async Task<RestaurantDto?> IRestaurantsService.GetRestaurantById(int id)
		{
			logger.LogInformation($"Getting restaurant with ID {id}");
			var restaurant = await restaurantsRepository.FindByIdAsync(id);
			var restaurantDto = RestaurantDto.FromEntity(restaurant);
			return restaurantDto;
		}
	}
}
