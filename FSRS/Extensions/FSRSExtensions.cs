using FSRS.Interfaces;
using FSRS.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FSRS.Extensions;

public static class FSRSExtensions
{
    public static IServiceCollection AddFSRS(this IServiceCollection services)
    {
        services.AddScoped<IScheduler, Scheduler>();

        return services;
    }
}
