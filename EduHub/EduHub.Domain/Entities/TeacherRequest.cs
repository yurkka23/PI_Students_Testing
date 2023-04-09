namespace EduHub.Domain.Entities;

public class TeacherRequest : BaseEntity
{
    public string Text { get; set; } = string.Empty;
    public string ProofImage { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}
