using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_backend.Models
{
    public class DrawingMessage
    {
        [Key]
        public int DrawId { get; set; }
        [ForeignKey("User")]
        public int FromUser { get; set; }
        [ForeignKey("User")]
        public int ToUser { get; set; }
        public byte[] Drawing { get; set; }
    }
}
