using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
	public interface IRestaurantsRepository
	{
		Task<IEnumerable<Restaurant>> GetAllByOwnerAsync(string ownerId);
		Task<IEnumerable<Restaurant>> GetAllAsync();
		Task<Restaurant?> FindByIdAsync(int id);
		Task<int> Create(Restaurant entity);
		Task Delete(Restaurant entity);
		Task Update(Restaurant entity);
		Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber);
	}
}
