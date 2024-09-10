﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishesForRestaurant;

public class DeleteAllDishesForRestaurantCommand(int restaurantId) : IRequest
{
	public int RestaurantId { get; } = restaurantId;
}
