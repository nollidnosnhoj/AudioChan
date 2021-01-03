﻿using System;
using System.Text;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Entities;
using Audiochan.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Audiochan.Web.Configurations
{
    public static class AuthConfigurationExtension
    {
        public static void AddIdentityAndAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = new JwtSetting();
            configuration.GetSection(nameof(JwtSetting)).Bind(jwtSetting);
            services.AddSingleton(jwtSetting);
            
            var passwordSetting = new PasswordSetting();
            configuration.GetSection(nameof(PasswordSetting)).Bind(passwordSetting);
            services.AddSingleton(passwordSetting);

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = passwordSetting.RequireLength;
                    options.Password.RequireDigit = passwordSetting.RequireDigit;
                    options.Password.RequireLowercase = passwordSetting.RequireLowercase;
                    options.Password.RequireUppercase = passwordSetting.RequireUppercase;
                    options.Password.RequireNonAlphanumeric = passwordSetting.RequireNonAlphanumeric;
                })
                .AddEntityFrameworkStores<AudiochanContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders();
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();
        }
    }
}