namespace infosysapi.Models
{
    public record Notare
    {
        public string Id { get; set; }

        public int StudentId { get; set; }

        public int CursId { get; set; }

        public int Scor { get; set; }
    }
}