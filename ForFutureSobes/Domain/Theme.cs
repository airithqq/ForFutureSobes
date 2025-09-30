namespace ForFutureSobes.Domain
{
    public class Theme
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
