using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Application.Interfaces.Services;
using EventManagement.Infrastructure.Implementations.Repositories;
using EventManagement.Infrastructure.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Infrastructure
{
    public static class InfrastructureDependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventRepository, EventRepository>();
        }
    }
}
