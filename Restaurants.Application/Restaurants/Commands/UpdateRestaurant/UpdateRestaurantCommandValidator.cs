using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100)
            .WithMessage("The name must have between 3 and 100 characters");

	    RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required");

	    RuleFor(dto => dto.HasDelivery)
                .NotNull()
                .WithMessage("Please indicate if the restaurant has delivery");

		RuleFor(x => x.Id)
			.Custom((id, context) =>
			{
				if (id != default)
				{
					context.AddFailure("Id should not be provided in the request");
				}
			});
	}	
}

