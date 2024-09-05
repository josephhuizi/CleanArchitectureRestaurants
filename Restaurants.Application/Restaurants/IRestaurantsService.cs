using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
	public interface IRestaurantsService
	{
		public Task<IEnumerable<RestaurantDto?>> GetAllRestaurants();

		public Task<RestaurantDto?> GetRestaurantById(int id);

		public Task<int> Create(CreateRestaurantDto dto);
	}
}
