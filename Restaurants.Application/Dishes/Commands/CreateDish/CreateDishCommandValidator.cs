using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
	public CreateDishCommandValidator() 
	{
		RuleFor(dish => dish.Name)
			.Length(3, 100)
			.WithMessage("The name must have between 3 and 100 characters");

		RuleFor(dish => dish.Description)
			.Length(3, 100)
			.WithMessage("The description must have between 3 and 100 characters");

		RuleFor(dish => dish.Price)
			.GreaterThanOrEqualTo(0)
			.WithMessage("Price must be a non-negative number.");

		RuleFor(dish => dish.KiloCalories)
			.GreaterThanOrEqualTo(0)
			.WithMessage("Kilocalories must be a non-negative number.");
	}
}
