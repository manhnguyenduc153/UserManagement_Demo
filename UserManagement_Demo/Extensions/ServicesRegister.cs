using Microsoft.AspNetCore.Authorization;
using UserManagement_Demo.Entities;
using UserManagement_Demo.Repositories;
using UserManagement_Demo.Repositories.IRepositories;
using UserManagement_Demo.Services;
using UserManagement_Demo.Services.IServices;

namespace UserManagement_Demo.Extensions
{
    public static class ServicesRegister
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
