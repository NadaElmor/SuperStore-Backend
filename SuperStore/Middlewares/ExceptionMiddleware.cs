using SuperStore.Errors;
using System.Net;
using System.Text.Json;

namespace SuperStore.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }catch(Exception ex)
            {
                
               //log in development mode
               logger.LogError(ex,ex.Message);
              //log in production mode
              //------

                //header of response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

                //get excpention error in specific format
                var Response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,
                    ex.Message,ex.StackTrace.ToString()) :new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(Response,options);
                //body of the response
                context.Response.WriteAsync(json);

            }
        }
    }
}
