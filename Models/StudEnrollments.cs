using System.ComponentModel.DataAnnotations;

namespace infosysapi.Models
{
    public record StudEnrollment
    {
        [Key]
        public string id { get; set; }

        public string studentid { get; set; }

        public string courseid { get; set; }
    }
}