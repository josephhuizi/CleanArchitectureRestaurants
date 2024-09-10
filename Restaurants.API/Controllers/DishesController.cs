using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteAllDishesForRestaurant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.API.Controllers;
[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediatr) : Controller
{
	[HttpPost]
	public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
	{
		command.RestaurantId = restaurantId;
		var dishId = await mediatr.Send(command);
		return CreatedAtAction(nameof(GetByIdForRestaurant), new {restaurantId, dishId}, null);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
	{
		var dishes = await mediatr.Send(new GetDishesForRestaurantQuery(restaurantId));
		return Ok(dishes);
	}

	[HttpGet("{dishId}")]
	public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
	{
		var dish = await mediatr.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
		return Ok(dish);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteAllDishesForRestaurant([FromRoute] int restaurantId)
	{
		await mediatr.Send(new DeleteAllDishesForRestaurantCommand(restaurantId));

		return NoContent();
	}
}
