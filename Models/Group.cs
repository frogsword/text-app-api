namespace TextApp.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid[] Members { get; set; }
        public string Name { get; set; }
    }
}
