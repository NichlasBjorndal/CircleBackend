using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_backend.Models
{
    public class User
    {
        [Key]
		public int UserId { get; set; } 
        [ForeignKey("Session")]
        public int SessionId { get; set; } 
        public string Name { get; set; } 
    }
}
