using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Tekus.BusinessLogic.Logic
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

         

        public string CreateToken(User usuario, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.LastName),
                new Claim("username", usuario.UserName),
            };

            if (roles != null && roles.Count > 0)
            {
                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }


            SigningCredentials credencials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenConfiguration = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(60),
                SigningCredentials = credencials,
                Issuer = _config["Token:Issuer"]
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenConfiguration);

            return tokenHandler.WriteToken(token);
        }
    }
}
