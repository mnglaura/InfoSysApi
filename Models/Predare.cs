namespace infosysapi.Models
{
    public record Predare
    {
        public string Id { get; set; }

        public int ProfesorId { get; set; }

        public int CursId { get; set; }
    }
}