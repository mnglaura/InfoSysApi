using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using infosysapi.Context;
using infosysapi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace infosysapi.Auth
{
    public class JWTManagerContext : IJWTManagerContext
    {
        // private List<Users> _users = new List<Users>
        // { 
        //     new Users { id = "jsh",  username = "admin", password = "admin", role = Roles.Admin },
        //     new Users { id = "asd",   username = "user", password = "user", role = Roles.Student } 
        // };
        private readonly IConfiguration iconfiguration;
        public JWTManagerContext(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public Tokens Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, user.role)                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);
            return new Tokens { token = tokenHandler.WriteToken(token) };
        }
    }

}