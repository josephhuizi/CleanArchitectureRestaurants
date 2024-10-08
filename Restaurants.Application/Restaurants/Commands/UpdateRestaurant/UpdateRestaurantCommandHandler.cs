﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
	internal class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
		IMapper mapper, IRestaurantAuthorizationService authorizationService, 
		IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand>
	{
		public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Updating restaurant with id : {request.Id}");
			var restaurant = await restaurantsRepository.FindByIdAsync(request.Id);

			if (restaurant is null)
				throw new RestaurantNotFoundException();

			if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
			{
				logger.LogWarning("The user is not allowed to update this restaurant");
				throw new ForbiddenException();
			}

			restaurant.Name = request.Name;
			restaurant.Description = request.Description;
			restaurant.HasDelivery = request.HasDelivery;

			await restaurantsRepository.Update(restaurant);
		}
	}
}
