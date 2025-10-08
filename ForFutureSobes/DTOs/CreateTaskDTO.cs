namespace ForFutureSobes.DTOs
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string ThemeName { get; set; }
        public bool IsCompleted { get; set; }
    }
}
