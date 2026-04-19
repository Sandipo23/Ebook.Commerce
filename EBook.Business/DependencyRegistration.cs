using EBook.Business.Interfaces;
using EBook.Business.Services.AdminServices;
using EBook.Business.Services.CustomerServices;

using Microsoft.Extensions.DependencyInjection;

namespace EBook.Business
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();

            return services;
        }
    }
}
