using System.ComponentModel.DataAnnotations;
namespace VSolx.View_Model
{
    public class LogVM
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        public string Msg { get; set; } = "";
    }
}
