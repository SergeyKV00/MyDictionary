using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDictionary.Api.Providers;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Application.Interfaces.Services;
using MyDictionary.Domain;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Infrastructure.Persistence;
using MyDictionary.Infrastructure.Persistence.Repostiories.UserDictionaries;
using MyDictionary.Infrastructure.Services;

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

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<SessionContext>();

        services.AddTransient<IUserDictionaryRepository, UserDictionaryRepository>();

        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }
}
