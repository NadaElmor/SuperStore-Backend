namespace SuperStore.Extentions
{
    public static class SwaggerExtenstions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
        public static WebApplication AddSwaggerMiddleWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
