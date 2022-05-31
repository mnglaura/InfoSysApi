using infosysapi.Dtos;
using infosysapi.Models;

namespace infosysapi.Extensions
{
    public static class StudentExtensions
    {
        public static StudentDto AsDto(this Student stud)
        {
            return new StudentDto()
            {
                firstname = stud.firstname,
                lastname = stud.lastname,
                email = stud.email
            };
        }
    }
}