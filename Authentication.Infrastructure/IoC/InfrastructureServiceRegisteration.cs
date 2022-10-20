using Authentication.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Infrastructure.Presistence;
using Authentication.Application.Common.Interfaces;
using Authenticaion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Authentication.Application.Common.Helpers;
using Microsoft.Extensions.Options;

namespace Authentication.Infrastructure.IoC
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationContext>(options =>
             options.UseSqlServer(configuration["ConnectionString"]));

            services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 8;

                opts.SignIn.RequireConfirmedEmail = true;
                //opts.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            });

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            Microsoft.Extensions.DependencyInjection.ServiceProvider sp = services.BuildServiceProvider();
            IOptions<AppSettings> iop = sp.GetService<IOptions<AppSettings>>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
       

            return services;
        }

    }
}
