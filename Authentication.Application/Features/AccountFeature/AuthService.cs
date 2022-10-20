using Authentication.Application.Features.AccountFeature.Interfaces;
using Authentication.Application.Features.AccountFeature.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Application.Features.AccountFeature
{
   
    public class AuthService : IAuthService
    {

        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        private readonly JWTOptions _jWTOptions;

      
        public AuthService(JWTOptions jWTOptions)
        {
            _jWTOptions = jWTOptions;
        }

        /// <summary>
        /// Method generate gwt token for user
        /// </summary>
        /// <param name="userClaim">taking claims email and password</param>
        /// <returns>gwt token</returns>
        public string GenerateToken(UserClaims userClaim, int expireMinutes = 1000000)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTOptions.Secret);
            var claims = new List<Claim> {
                    new Claim("uid", userClaim.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uemail", userClaim.Email),
                    new Claim(ClaimTypes.Name, userClaim.Email),
                    new Claim(ClaimTypes.NameIdentifier, userClaim.Id.ToString()),
                     new Claim(ClaimTypes.Role, userClaim.RoleName)
                };

            //var user = _userManager.FindByNameAsync(userClaim.Email).Result;

            //var userClaims = _userManager.GetClaimsAsync(user).Result;
            //claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jWTOptions.TokenLifeTime),
                Issuer = _jWTOptions.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);



        }
        public bool IsValidToken(string token, String[] roles)
        {
            String userName, role, id;

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated)
                throw new UnauthorizedAccessException();


            id = identity.FindFirst(ClaimTypes.Sid)?.Value;
            userName = identity.FindFirst(ClaimTypes.Name)?.Value;
            role = identity.FindFirst(ClaimTypes.Role)?.Value;

            if (roles.All(r => r != role))
                throw new UnauthorizedAccessException();

            return true;
        }
        public UserClaims GetUserPrincipal(String token)
        {
            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated)
                throw new UnauthorizedAccessException();


            return new UserClaims
            {
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                RoleName = identity.FindFirst(ClaimTypes.Role)?.Value,
            };
        }
        private ClaimsPrincipal GetPrincipal(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = Convert.FromBase64String(Secret);

            var validationParameters = new TokenValidationParameters()
            {

                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

            return principal;
        }

    }
}
