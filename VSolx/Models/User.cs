using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VSolx.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "max length is 20 char")]
        public string fname { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "max length is 20 char")]
        public string lname { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression("^01[0125][0-9]{8}")]
        public string Phone { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "password does not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*")]
        public string Role { get; set; }
        public string? Visa { get; set; }
        public string? work { get; set; }
        // Navigation Property
        public List<Product> Products { get; set; }
        public List<Comment> comments { get; set; }
        public List<Cart> cart { get; set; }
    }
}
