using Application.Abstractions.Token;
using Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,TokenHandler>();
        }
    }
}
