using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
	public class CheckExecutionTimeMiddleware(ILogger<CheckExecutionTimeMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			
			var stopWatch = Stopwatch.StartNew();

			await next.Invoke(context);

			stopWatch.Stop();

			if (stopWatch.ElapsedMilliseconds > 2000)
			{
				var path = context.Request.Path.ToString();
				var method = context.Request.Method.ToString();
				logger.LogInformation($"Please take care of the route path {path} method {method}. Took {stopWatch.ElapsedMilliseconds}ms to complete");
			}
		}
	}
}
