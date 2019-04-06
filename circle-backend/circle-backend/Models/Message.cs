using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_backend.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [ForeignKey("Session")]
        public int SessionId { get; set; }
        [ForeignKey("User")]
        public int FromUser { get; set; }
        [ForeignKey("User")]
        public int ToUser { get; set; }
        public byte[] Payload { get; set; }
    }
}
