using CleanArchitecture.Application.Ecommerce.IServices.Roles;
using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Ecommerce.Services.Roles;
using CleanArchitecture.Application.Ecommerce.Services.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Users;
using CleanArchitecture.Infrastructure.Ecommerce.Repository.UserModule.Roles;
using CleanArchitecture.Infrastructure.Ecommerce.Repository.UserModule.Users;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion User End

            #region Role
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion Role End

            return services;
        }
    }
}
