using AutoMapper;
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

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
	IRestaurantsRepository restaurantsRepository, 
	IRestaurantAuthorizationService authorizationService) : IRequestHandler<DeleteRestaurantCommand>
{
	public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation($"Deleting restaurant with id : { request.Id }");
		var restaurant = await restaurantsRepository.FindByIdAsync( request.Id );
		if (restaurant is null)
			throw new RestaurantNotFoundException();

		if (!authorizationService.Authorize(restaurant, ResourceOperation.Delete))
			throw new ForbiddenException();

		await restaurantsRepository.Delete(restaurant);
		
	}
}
