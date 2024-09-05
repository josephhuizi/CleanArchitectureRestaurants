﻿using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("api/restaurants")]
	public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var restaurants = await restaurantsService.GetAllRestaurants();
			return Ok(restaurants);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetRestaurant([FromRoute] int id)
		{
			var restaurant = await restaurantsService.GetRestaurantById(id);
			if (restaurant == null)
				return NotFound();
			return Ok(restaurant);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
		{
			int id = await restaurantsService.Create(createRestaurantDto);
			return CreatedAtAction(nameof(GetRestaurant), new { id }, null);
		}
	}
}
