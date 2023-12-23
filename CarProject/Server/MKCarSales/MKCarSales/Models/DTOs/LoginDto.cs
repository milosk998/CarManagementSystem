using System.ComponentModel.DataAnnotations;

namespace MKCarSales.Models.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string Password { get; set; }
}
