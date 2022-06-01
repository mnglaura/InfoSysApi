using infosysapi.Models;

namespace infosysapi.Auth
{
       public interface IJWTManagerContext
    {
        Tokens Authenticate(User user); 
    }
}