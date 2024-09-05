﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
	IMapper mapper, 
	IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
	public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation($"Deleting restaurant with id : { request.Id }");
		var restaurant = await restaurantsRepository.FindByIdAsync( request.Id );
		if (restaurant is null)
			return false;

		await restaurantsRepository.Delete(restaurant);
		return true;
		
	}
}
