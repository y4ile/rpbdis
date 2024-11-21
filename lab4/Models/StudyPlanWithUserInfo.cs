namespace lab4.Models
{
    public class StudyPlanWithUserInfo
    {
        public int PlanID { get; set; }

        public string PlanName { get; set; } = null!;

        public string DirectionName { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
