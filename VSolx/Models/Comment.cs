using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VSolx.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Body { get; set; }

        public string? Sticker { get; set; }
        [ForeignKey("product")]
        public int ProductID { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }

        // Navigation Property
        public Product product { get; set; }
        public User user { get; set; }
    }
}
