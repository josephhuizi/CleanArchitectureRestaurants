using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
	IMapper mapper, IRestaurantsRepository restaurantsRepository, 
	IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand, int>
{
	public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating new dish: {@dishRequest}", request);
		var restaurant = await restaurantsRepository.FindByIdAsync(request.RestaurantId);
		if (restaurant == null) throw new RestaurantNotFoundException();

		var dish = mapper.Map<Dish>(request);

		return await dishesRepository.Create(dish);
	}
}
