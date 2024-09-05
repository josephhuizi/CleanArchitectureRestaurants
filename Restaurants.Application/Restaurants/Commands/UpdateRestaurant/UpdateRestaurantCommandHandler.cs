using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
	internal class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
		IMapper mapper,
		IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
	{
		public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Updating restaurant with id : {request.Id}");
			var restaurant = await restaurantsRepository.FindByIdAsync(request.Id);

			if (restaurant is null)
				return false;

			restaurant.Name = request.Name;
			restaurant.Description = request.Description;
			restaurant.HasDelivery = request.HasDelivery;

			await restaurantsRepository.Update(restaurant);
			return true;
		}
	}
}
