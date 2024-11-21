namespace lab4.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    }
}
