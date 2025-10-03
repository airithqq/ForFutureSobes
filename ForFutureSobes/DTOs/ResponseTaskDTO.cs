namespace ForFutureSobes.DTOs
{
    public class ResponseTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public string ThemeName { get; set; }

        public string Priority { get; set; }
    }
}
