﻿using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares;
public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next.Invoke(context);
		}
		catch(DomainException domainException)
		{
			context.Response.StatusCode = 400;
			await context.Response.WriteAsJsonAsync(new
			{
				Error = new
				{
					Type = domainException.Type,
					Message = domainException.Message
				}
			});
		}
		catch (NotFoundException notFound)
		{
			context.Response.StatusCode = 404;
			await context.Response.WriteAsync(notFound.Message);

			logger.LogWarning(notFound.Message);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, ex.Message);

			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Something went wrong");
		}
	}
}
