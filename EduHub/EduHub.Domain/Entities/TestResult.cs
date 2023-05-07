namespace EduHub.Domain.Entities
{
    public class TestResult : BaseEntity
    {
        public double PercentageCorrrectAnswers { get; set; }

        public double SumPoints { get; set; }

        //relations
        public Guid StudentId { get; set; }
        public User Student { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
    }
}