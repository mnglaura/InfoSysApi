using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace infosysapi.Models
{
    public record Homework
    {
        [Key]
        public string id { get; set; }

        [Required]
        public string courseid { get; set; }

        [Required]
        public string studentid { get; set; }

        [Required]
        public string status { get; set; }

        [AllowNull]
        public DateTime? submissiondate { get; set; }
    }
}