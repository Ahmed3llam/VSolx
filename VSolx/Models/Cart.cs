using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VSolx.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("user")]
        public int UserID { get; set; }
        [ForeignKey("product")]
        public int ProductID { get; set; }
        public int Count { get; set; }
        public int Tprice { get; set; }
        public User user { get; set; }
        public Product product { get; set; }
    }
}
