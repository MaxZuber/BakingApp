using System.ComponentModel.DataAnnotations;


namespace Banking.Models
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
