using System.ComponentModel.DataAnnotations;

namespace infosysapi.Dtos
{
    public class StudentDto
    {
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        public string email { get; set; }
    }
}