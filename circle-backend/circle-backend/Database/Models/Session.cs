using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace circle_backend.Models
{
	public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Code { get; set; }
        public bool HasStarted { get; set; }
        public List<DrawingMessage> DrawingMessages { get; set; } //int is DrawingMessage.DrawId
        public List<TextMessage> TextMessages { get; set; } //int is TextMessage.Textid
    }
}