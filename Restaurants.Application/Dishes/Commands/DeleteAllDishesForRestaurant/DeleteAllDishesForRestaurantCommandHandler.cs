using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishesForRestaurant;

public class DeleteAllDishesForRestaurantCommandHandler(ILogger<DeleteAllDishesForRestaurantCommandHandler> logger, 
	IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteAllDishesForRestaurantCommand>
{
	public async Task Handle(DeleteAllDishesForRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Deleting all dishes for restaurant {restaurantId}", request.RestaurantId);

		var restaurant = await restaurantsRepository.FindByIdAsync(request.RestaurantId);
		if (restaurant == null) throw new NotFoundException("The restaurant doesn't exist");

		await dishesRepository.DeleteAll(restaurant.Dishes);
	}
}
