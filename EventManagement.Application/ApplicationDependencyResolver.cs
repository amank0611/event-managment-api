using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Application
{
    public static class ApplicationDependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            //services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
