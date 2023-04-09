﻿namespace EduHub.Application.DTOs.Admin;

public class TeacherRequestDTO
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string ProofImage { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}