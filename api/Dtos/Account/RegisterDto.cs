using System.ComponentModel.DataAnnotations;

namespace api.Dto.Account;

public class RegisterDto
{
    [Required]
    [MaxLength(20, ErrorMessage = "Username cannot be over 20 characters")]
    public string? UserName { get; set; }  
    [Required]
    [EmailAddress]
    public string? Email { get; set;}
    [Required]
    public string? Password { get; set;}
}