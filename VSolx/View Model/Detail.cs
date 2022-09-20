using VSolx.Models;

namespace VSolx.View_Model
{
    public class Detail
    {
        public int id { get; set; }

        public int Uid { get; set; }

        public int Pid { get; set; }
        public User User { get; set; }

        public List<Product> Products { get; set; }
        public List<Comment> comments { get; set; }
        public Product Product { get; set; }
        public Comment Comment { get; set; }

    }
}
