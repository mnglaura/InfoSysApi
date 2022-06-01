using System.ComponentModel.DataAnnotations;

namespace infosysapi.Models
{
    public record Cours
    {
        [Key]
        public string id { get; set; }

        public string name { get; set; }

        public int semester { get; set; }
    }
}