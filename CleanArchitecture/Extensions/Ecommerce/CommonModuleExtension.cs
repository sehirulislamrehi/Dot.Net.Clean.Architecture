using CleanArchitecture.Domain.Ecommerce.IRepository.CommonModule;
using CleanArchitecture.Infrastructure.Ecommerce.Repository.CommonModule;

namespace CleanArchitecture.Extensions.Ecommerce
{
    public static class CommonModuleExtension
    {
        public static IServiceCollection AddCommonModuleServices(this IServiceCollection services)
        {

            #region Module
            services.AddScoped<IModuleRepository, ModuleRepository>();
            #endregion Module End

            return services;
        }
    }
}
