using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDictionary.Application.Interfaces;
using MyDictionary.Infrastructure.Persistence;

namespace MyDictionary.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString"];
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b =>
                b.MigrationsAssembly("MyDictionary.Infrastructure"));
        });       

        services.AddScoped<IAppDbContext>(provider => 
            provider.GetRequiredService<AppDbContext>());

        return services;
    }
}
