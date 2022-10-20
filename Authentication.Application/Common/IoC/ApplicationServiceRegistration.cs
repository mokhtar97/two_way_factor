using Authentication.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Authentication.Application.Common.Behaviours;
using Authentication.Application.Features.AccountFeature.Interfaces;
using Authentication.Application.Features.AccountFeature;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Authentication.Application.Common.Behaviors;
using Authentication.Infrastructure.NotificationServices.SMSService.Helpers;

namespace Authentication.Application.Common.IoC
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(new[] { Assembly.GetExecutingAssembly() });
            
                 services.AddScoped<ISmsTwilio, SmsTwilio>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
           

            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped<IMailService, MailService>();
            return services;
        }
    }
}
