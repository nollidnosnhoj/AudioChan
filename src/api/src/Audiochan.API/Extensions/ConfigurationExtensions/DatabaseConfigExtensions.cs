﻿
using Audiochan.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Audiochan.API.Extensions.ConfigurationExtensions;

public static class DatabaseConfigExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment env)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("Database"));
            o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            o.UseSnakeCaseNamingConvention();
            if (env.IsDevelopment())
            {
                o.EnableSensitiveDataLogging();
            }
        });
        return services;
    }
}