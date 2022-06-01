namespace infosysapi.Auth
{
       public interface IJWTManagerContext
    {
        Tokens Authenticate(Users users); 
    }
}