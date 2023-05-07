using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Auth;

public class EmailModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
}
