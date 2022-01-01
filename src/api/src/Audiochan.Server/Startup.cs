using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Audiochan.Server.Extensions.ConfigurationExtensions;
using Audiochan.Application;
using Audiochan.Application.Commons.Pipelines;
using Audiochan.Application.Services;
using Audiochan.GraphQL;
using Audiochan.Infrastructure;
using Audiochan.Infrastructure.Storage.AmazonS3;
using Audiochan.Server.Middlewares;
using Audiochan.Server.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Audiochan.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AmazonS3Settings>(Configuration.GetSection(nameof(AmazonS3Settings)));
            services.Configure<MediaStorageSettings>(Configuration.GetSection(nameof(MediaStorageSettings)));
            services.Configure<IdentitySettings>(Configuration.GetSection(nameof(IdentitySettings)));

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = false
            };

            services.AddMemoryCache();
            services.AddApplication(Configuration, Environment);
            services.AddInfrastructure(Configuration, Environment);
            services.AddGraphQl();
            services.Configure<JsonSerializerOptions>(options =>
            {
                options.DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition;
                options.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
                options.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
            });
            services.ConfigureAuthentication(Configuration, Environment);
            services.ConfigureAuthorization();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.ConfigureControllers(jsonSerializerOptions);
            services.ConfigureRouting();
            services.ConfigureRateLimiting(Configuration);
            services.ConfigureCors();
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsConfig();
            app.UseRateLimiting();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
            });

            app.UseSwaggerConfig();
        }
    }
}