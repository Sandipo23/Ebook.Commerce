using EBook.Business.Interfaces;
using EBook.Business.Services.CustomerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Ebook.Web
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddWebDependencies(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();

            return services;
        }
    }
}
