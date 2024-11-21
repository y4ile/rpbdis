namespace lab4.Models
{
    public class DevelopmentDirection
    {
        public int DirectionID { get; set; }

        public string DirectionName { get; set; } = null!;

        public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    }
}
