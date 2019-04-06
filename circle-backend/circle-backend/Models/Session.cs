using System.Collections.Generic;

namespace circle_backend.Models
{
	public class Session
    {
        public int SessionId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
