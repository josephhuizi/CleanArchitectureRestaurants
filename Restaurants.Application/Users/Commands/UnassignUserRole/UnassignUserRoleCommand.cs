using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
	public required string UserEmail { get; set; }
	public required string RoleName { get; set; }
}
