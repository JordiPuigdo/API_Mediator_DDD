using Database.Repositories;
using Domain.Abstractions.Providers;
using Domain.Interfaces.Accounts;
using Domain.Interfaces.Contacts;
using Domain.Interfaces.Users;
using Domain.SharedModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Database
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services)
        {
            services.AddScoped<ICommonRepository, UserRepository>();
            services.AddScoped<IUsersRepository, UserRepository>();
            services.AddScoped<ICommonRepository, ContactRepository>();
            services.AddScoped<IContactsRepository, ContactRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}
