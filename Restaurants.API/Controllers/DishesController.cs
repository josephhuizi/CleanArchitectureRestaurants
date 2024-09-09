using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.API.Controllers;
[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediatr) : Controller
{
	[HttpPost]
	public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
	{
		command.RestaurantId = restaurantId;
		await mediatr.Send(command);
		return Created();
	}
}
