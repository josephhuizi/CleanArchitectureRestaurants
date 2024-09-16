
namespace Restaurants.Domain.Exceptions;

public class RestaurantNotFoundException : DomainException
{
    public RestaurantNotFoundException() : base("The restaurant is not registered", nameof(RestaurantNotFoundException))
    {
    }
}
