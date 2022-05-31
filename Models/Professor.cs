using System.ComponentModel.DataAnnotations;

namespace infosysapi.Models
{
    public record Professor
    {
        [Key]
        public string id { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string email { get; set; }
    }
}