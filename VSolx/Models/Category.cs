using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace VSolx.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        // Navigation Property
        public List<Product> Products { get; set; }
    }
}
