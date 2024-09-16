
namespace Restaurants.Domain.Exceptions;

public class RestaurantNotFoundException : BusinessRuleException
{
    public RestaurantNotFoundException() : base("The restaurant is not registered", nameof(RestaurantNotFoundException))
    {
    }
}
