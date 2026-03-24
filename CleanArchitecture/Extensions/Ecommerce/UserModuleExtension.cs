using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Ecommerce.Services.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.Users;
using CleanArchitecture.Infrastructure.Ecommerce.Repository.Users;

namespace CleanArchitecture.Extensions.Ecommerce
{
    public static class UserModuleExtension
    {
        public static IServiceCollection AddUserModuleServices(this IServiceCollection services)
        {

            #region Auth
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            #endregion Auth End

            #region User
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion User End

            return services;
        }
    }
}
