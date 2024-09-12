using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

internal class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger,
	UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{
	public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Unassigning user role: {@request}", request);

		var user = await userManager.FindByEmailAsync(request.UserEmail) ??
			throw new NotFoundException("Not found user");

		var role = await roleManager.FindByNameAsync(request.RoleName) ??
			throw new NotFoundException("The role doesn't exist");

		await userManager.RemoveFromRoleAsync(user, role.Name!);
	}
}
