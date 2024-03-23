using Core.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.TokenService
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey _key;

        //to find the appsetting.json we have to inject the Iconfiguration interface

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
            //reading the key as bytes to be ready for passing to creds in byte form 
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])); 
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName , appUser.DisplayName)
            };
            var creds = new SigningCredentials(_key , SecurityAlgorithms.HmacSha256); //hashing

            var tokenDescriptor = new SecurityTokenDescriptor
            {
               Subject= new ClaimsIdentity(claims),
                Issuer = configuration["Token:Issuer"],
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials= creds
            };
            var TokenHandler = new JwtSecurityTokenHandler(); //Token handler for creating the token
            var Token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken (Token);
        }
    }
}
