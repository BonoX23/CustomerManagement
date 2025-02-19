using Application.AppServices;
using Domain.Interfaces.Repository;
using Domain.Interfaces;
using Domain.Notifications;
using Repository.Repositories;
using Repository.Contexts;

namespace WebApi.Configuration
{
    public static class InjectDependencyConfig
    {
        public static void InjectDependencyRegister(this IServiceCollection services)
        {
            services.AddScoped<CustomerDbContext>();
            services.AddScoped<INotificationContext, NotificationContext>();


            #region AppServices

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressService, AddressService>();

            #endregion

            #region Repositories

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            #endregion
        }
    }
}
