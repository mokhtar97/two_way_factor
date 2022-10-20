using AutoMapper;
using Authentication.Application.Common.Interfaces;
using Authentication.Application.Common.Pagination;
using System;
using System.Linq;
using System.Reflection;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authenticaion.Domain.Entities;
using Authenticaion.Application.Common.ValueResolvers;

namespace Authentication.Application.Common.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(d => d.role, opt => opt.MapFrom<UserDtoRoleResolver>());
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<EditUserDto, ApplicationUser>();

         
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var mapInterface = type.GetInterfaces()[0];
                var genericType = mapInterface.GetGenericArguments()[0];
                var instance = Activator.CreateInstance(type);
                var mapType = typeof(IMapFrom<>).MakeGenericType(genericType);
                var methodInfo = mapType.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
