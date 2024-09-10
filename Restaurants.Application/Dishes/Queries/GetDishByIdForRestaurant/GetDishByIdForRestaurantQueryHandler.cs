using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
	IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
	public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Retrieving dish {dishId} for restaurant with id: {restaurantId}", 
			request.DishId,
			request.RestaurantId);
		var restaurant = await restaurantsRepository.FindByIdAsync(request.RestaurantId);

		if (restaurant == null) throw new NotFoundException("The restaurant doesn't exist");

		var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

		if (dish == null) throw new NotFoundException("The dish doesn't exist");

		var result = mapper.Map<DishDto>(dish);

		return result;
	}
}
