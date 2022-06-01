using System.ComponentModel.DataAnnotations;

namespace infosysapi.Models
{
    public record Grade
    {
        [Key]
        public string id { get; set; }

        public string studentid { get; set; }

        public string courseid { get; set; }

        public int grade { get; set; }
    }
}