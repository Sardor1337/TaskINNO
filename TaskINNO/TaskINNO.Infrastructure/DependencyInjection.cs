using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskINNO.Application.Abstractions;
using TaskINNO.Infrastructure.Persistence;

namespace TaskINNO.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
