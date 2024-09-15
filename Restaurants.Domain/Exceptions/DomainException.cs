namespace Restaurants.Domain.Exceptions;

public class DomainException(string message, string type) : Exception(message)
{
    public string Type { get; set; } = type;
}
