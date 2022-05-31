using System;
using System.ComponentModel.DataAnnotations;

namespace infosysapi.Models
{
    public record Tema
    {
        public string Id { get; set; }

        [Required]
        public int CursId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public string Status { get; set; }

        public DateTimeOffset SubmissionDate { get; set; }
    }
}