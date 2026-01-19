using System.ComponentModel.DataAnnotations;

namespace Exam4.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        [Required]
        public string? Password { get; set; }
      
    }
}
