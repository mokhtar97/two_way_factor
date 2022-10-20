using AutoMapper;
using Authentication.Application.Features.AccountFeature.Interfaces;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Authenticaion.Domain.Entities;

namespace Authentication.Application.Features.AccountFeature
{
    
    public class TokenService : ITokenService
    {
        private readonly JWTOptions _jWTOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public TokenService(JWTOptions jWTOptions, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _jWTOptions = jWTOptions;
            _userManager = userManager;
            _mapper = mapper;
        }
        private TimeSpan ExpiryDuration = new TimeSpan(72, 30, 0);
        public async Task<string> BuildTokenAsync(ApplicationUser user)
        {
            var userData = _mapper.Map<UserDto>(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userData.role),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim("profile",
                        JsonSerializer.Serialize(userData,
                            new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}))
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTOptions.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(_jWTOptions.Issuer, _jWTOptions.Audience, claims,
            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
