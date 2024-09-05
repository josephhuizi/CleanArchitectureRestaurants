using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

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

		[HttpGet]
		[Route("{restaurantId}")]
		public async Task<IActionResult> GetRestaurant(int restaurantId)
		{
			var restaurant = await restaurantsService.GetRestaurantById(restaurantId);
			if (restaurant == null)
				return BadRequest();
			return Ok(restaurant);
		}
	}
}
