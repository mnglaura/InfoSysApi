using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace infosysapi.Auth
{
    public class JWTManagerContext : IJWTManagerContext
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"},
        };

        private readonly IConfiguration iconfiguration;
        public JWTManagerContext(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        
        public Tokens Authenticate(Users users)
        {
            if (!UsersRecords.Any(x => x.Key == users.username && x.Value == users.password)) 
            {
			    return null;
		    }

            // else generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.username)                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { token = tokenHandler.WriteToken(token) };
        }
    }

}