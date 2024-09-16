namespace Restaurants.Domain.Exceptions;

public class BusinessRuleException(string message, string type) : Exception(message)
{
    public string Type { get; set; } = type;
}
