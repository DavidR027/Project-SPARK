using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
