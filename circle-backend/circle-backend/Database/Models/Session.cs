using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_backend.Models
{
	public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Code { get; set; }
        public bool HasStarted { get; set; }
        public ICollection<DrawingMessage> DrawingMessages { get; set; }
        public ICollection<TextMessage> TextMessages { get; set; }
    }
}
