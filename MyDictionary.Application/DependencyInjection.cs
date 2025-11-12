using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyDictionary.Application.Common.Behaviors;
using MyDictionary.Domain.UserDictionaries;
using System.Reflection;

namespace MyDictionary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserDictionaryService, UserDictionaryService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
