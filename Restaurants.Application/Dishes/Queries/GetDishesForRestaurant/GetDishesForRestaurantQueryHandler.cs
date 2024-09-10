﻿using AutoMapper;
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

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger, 
	IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
	public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Retrieving dishes for restaurant with id: {restaurantId}", request.RestaurantId);
		var restaurant = await restaurantsRepository.FindByIdAsync(request.RestaurantId);

		if (restaurant == null) throw new NotFoundException("The restaurant doesn't exist");

		var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

		return results;
	}
}
