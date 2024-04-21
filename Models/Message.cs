using System.ComponentModel.DataAnnotations.Schema;

namespace TextApp.Models
{
    [Table("Message")]
    public class Message
    {
        public Guid Id { get; set; }
        public Guid Sender {  get; set; }
        public Guid GroupId { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
