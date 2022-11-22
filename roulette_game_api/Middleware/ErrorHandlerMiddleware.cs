using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace roulette_game_api.Middleware
{
    public class ErrorHandlerMiddleware:IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            this.logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ProblemDetails problemDetails = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = ex.GetType().Name,
                    Type = ex.GetType().Name,
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(problemDetails);
                await context.Response.WriteAsync(json);
                context.Response.ContentType= "application/json";
            }
        }
    }
}
