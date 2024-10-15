using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Store.Core.Entities;
using SuperStore.Core;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Services.Contracts;
using SuperStore.Errors;
using SuperStore.Helper;
using SuperStore.Repositery;
using SuperStore.Services;
using System.Runtime.CompilerServices;

namespace SuperStore.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            //allow DI for IOrderService
            Services.AddScoped(typeof(IOrderService), typeof(OrderServie));
            //allow DI for Iunitofwork
            Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            // allow dependency injection for product and any other type
            Services.AddScoped(typeof(IGenericRepositery<>), typeof(GenericRepositery<>));
            //allow mapping
            Services.AddAutoMapper(typeof(MappingProfile));
            //allow DI for IPaymentService
            Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            // validation error response handling
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();
                    var ValidationErrorResponse = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });
            
            //allow Di for Basket Repositery
            Services.AddScoped<IBasketRepositery,BasketRepositery>();
            return Services;
        }
    }
}
