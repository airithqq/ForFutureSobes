namespace ForFutureSobes.Model.Domain
{
    public class TaskEntity
    {
        public Theme Theme { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int ThemeId {  get; set; }
        public string Priority { get; set; }
      
    }
}
