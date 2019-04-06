using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_backend.Models
{
    [Serializable]
    public class TextMessage
    {
        [Key]
        public int TextId { get; set; }
        [ForeignKey("User")]
        public int FromUser { get; set; }
        [ForeignKey("User")]
        public int ToUser { get; set; }
        public string Message { get; set; }
    }
}