using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumRestaurantsRequirementHandler(ILogger<MinimumRestaurantsRequirementHandler> logger, 
	IUserContext userContext, IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantsRequirement>
{
	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequirement requirement)
	{
		var currentUser = userContext.GetCurrentUser();
		logger.LogInformation("User: {Email} - Handling MinimumRestaurantsRequirements",
			currentUser!.Email);

		// Get restaurants from this user

		var restaurantsByOwner = await restaurantsRepository.GetAllByOwnerAsync(currentUser.Id);

		if (restaurantsByOwner.Count() >= requirement.RestaurantsNumber)
			context.Succeed(requirement);
		else
			context.Fail();

		await Task.CompletedTask;

	}
}
