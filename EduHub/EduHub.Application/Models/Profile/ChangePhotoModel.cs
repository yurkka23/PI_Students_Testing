using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Profile;

public class ChangePhotoModel
{
    [Required]
    public IFormFile NewProfileImage { get; set; }

    public string? UserImgUrl { get; set; }
}
