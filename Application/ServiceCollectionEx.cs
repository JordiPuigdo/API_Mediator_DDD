using Application.Users;
using Domain.SharedModels;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.Users;
using Domain.User;
using Domain.Authentication;
using Application.Authentication;
using MediatR;
using Application.CQRS.User;
using Application.Abstractions.Behaviors;
using Domain.Abstractions.Providers;
using Application.Abastractions.Behaviors;
using Domain.Abstractions;

namespace Application
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<ICommonService, UsersService>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));


            return services;

        }
    }
}
