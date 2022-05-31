using infosysapi.Dtos;
using infosysapi.Models;

namespace infosysapi.Extensions
{
    public static class ProfessorExtensions
    {
        public static ProfessorDto AsDto(this Professor prof)
        {
            return new ProfessorDto()
            {
                firstname = prof.firstname,
                lastname = prof.lastname,
                email = prof.email
            };
        }
    }
}