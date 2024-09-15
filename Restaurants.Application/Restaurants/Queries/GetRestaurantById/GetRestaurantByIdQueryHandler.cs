﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;
public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
	IMapper mapper, IRestaurantsRepository restaurantsRepository
	) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
	public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation($"Getting restaurant with ID {request.Id}");
		var restaurant = await restaurantsRepository.FindByIdAsync(request.Id) ??
			throw new RestaurantNotFoundException();
		var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
		return restaurantDto;
	}
}
