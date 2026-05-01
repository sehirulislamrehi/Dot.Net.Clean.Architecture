using CleanArchitecture.Application.Ecommerce.IServices.CommonModule;
using CleanArchitecture.Application.Ecommerce.Services.CommonModule;
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
            services.AddScoped<IModuleService, ModuleService>();
            #endregion Module End

            return services;
        }
    }
}
