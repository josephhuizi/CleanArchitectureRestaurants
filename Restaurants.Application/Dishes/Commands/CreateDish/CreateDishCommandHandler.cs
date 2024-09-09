using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
	IMapper mapper, IRestaurantsRepository restaurantsRepository, 
	IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand>
{
	public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating new dish: {@dishRequest}", request);
		var restaurant = await restaurantsRepository.FindByIdAsync(request.RestaurantId);
		if (restaurant == null) throw new NotFoundException("Restaurant not found");

		var dish = mapper.Map<Dish>(request);

		await dishesRepository.Create(dish);
	}
}
