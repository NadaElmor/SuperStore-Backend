using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using SuperStore.Core.Entities.User;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Services.Contracts;
using SuperStore.Errors;
using SuperStore.Extentions;
using SuperStore.Helper;
using SuperStore.Middlewares;
using SuperStore.Repositery;
using SuperStore.Repositery.Identity;
using SuperStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerService();
//allow dependecy injection 
builder.Services.AddDbContext<SuperStoreDbContext>(Options =>
         Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
         );
//allow DI for IconnectionMultiplexer
builder.Services.AddSingleton<IConnectionMultiplexer>(ServiceProvider =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connection);
});
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

//allow DI for Dbcontext Identity
builder.Services.AddDbContext<AppIdentityDbContext>(OptionBuilder =>
{
    OptionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddAuthServices(builder.Configuration);


var app = builder.Build();


#region Update database

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var dbcontext = services.GetRequiredService<SuperStoreDbContext>();
    await dbcontext.Database.MigrateAsync();
    await SuperStoreDbContextSeeding.DataSeed(dbcontext);

    //identity DbContext
    var IdentityDbContext=services.GetRequiredService<AppIdentityDbContext>();
    await IdentityDbContext.Database.MigrateAsync();
    var usermanager = services.GetRequiredService<UserManager<AppUser>>();
    await IdentityDbContextSeed.SeedUserAsync(usermanager);
}
catch (Exception ex)
{
    var logger = LoggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "Update database error");
}

#endregion


app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AddSwaggerMiddleWare();
}


app.UseStatusCodePagesWithRedirects("/Errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
