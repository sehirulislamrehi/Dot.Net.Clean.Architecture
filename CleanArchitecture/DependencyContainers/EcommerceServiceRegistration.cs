using CleanArchitecture.Extensions.Ecommerce;

namespace CleanArchitecture.DependencyContainers
{
    public static class EcommerceServiceRegistration
    {
        public static IServiceCollection AddEcommerceServices(this IServiceCollection services)
        {
            services.AddUserModuleServices();
            services.AddCommonModuleServices();
            return services;
        }
    }
}
