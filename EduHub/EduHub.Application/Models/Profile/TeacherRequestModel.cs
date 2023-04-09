

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Profile;

public class TeacherRequestModel
{
    [Required]
    public IFormFile ProofImage { get; set; }

    [Required]
    public string Text { get; set; }
}
