using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VSolx.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public string Body { get; set; }

        [Required(ErrorMessage = "*")]
        public string Img { get; set; }

        [Required(ErrorMessage = "*")]
        public int Price { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }

        //[Required(ErrorMessage = "*")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category  Category { get; set; }

        // Navigation Property
        public User User { get; set; }
        
        public List<Comment> Comments { get; set; }
        public List<Cart> Cart { get; set; }

    }
}
