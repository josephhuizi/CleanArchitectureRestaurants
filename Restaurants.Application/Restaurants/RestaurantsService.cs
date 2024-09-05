using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
	internal class RestaurantsService(
		IRestaurantsRepository restaurantsRepository, 
		ILogger<RestaurantsService> logger,
		IMapper mapper
	) : IRestaurantsService
	{
		public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurants()
		{
			logger.LogInformation("Getting all restaurants");
			var restaurants = await restaurantsRepository.GetAllAsync();

			var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
			return restaurantsDto;
		}

		async Task<int> IRestaurantsService.Create(CreateRestaurantDto dto)
		{
			logger.LogInformation("Creating a new restaurant");
			var restaurant = mapper.Map<Restaurant>(dto);
			int id = await restaurantsRepository.Create(restaurant);
			return id;
		}

		async Task<RestaurantDto?> IRestaurantsService.GetRestaurantById(int id)
		{
			logger.LogInformation($"Getting restaurant with ID {id}");
			var restaurant = await restaurantsRepository.FindByIdAsync(id);
			var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
			return restaurantDto;
		}
	}
}
