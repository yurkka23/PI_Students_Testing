using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EduHub.Application.Models.Profile
{
    public class ChangePhotoModel
    {
        [Required] public IFormFile NewProfileImage { get; set; }

        public string? UserImgUrl { get; set; }
    }
}