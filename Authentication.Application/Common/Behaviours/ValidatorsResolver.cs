using Microsoft.Extensions.DependencyInjection;

namespace Authenticaion.Application.Common.IoC
{
    public static class ValidatorsResolver
    {
        public static void ResolveValidators(this IServiceCollection services)
        {
            //services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            //services.AddTransient<IValidator<CustomerDto>, CustomerDtoValidator>();
        }
    }
}
