using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EduHub.Application.Models.Profile
{
    public class TeacherRequestModel
    {
        [Required] public IFormFile ProofImage { get; set; }

        [Required] public string Text { get; set; }
    }
}