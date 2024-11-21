namespace lab4.Models
{
    public class UsersWithStudyPlan
    {
        public int UserID { get; set; }

        public string Login { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int PlanID { get; set; }

        public string PlanName { get; set; } = null!;

        public string DirectionName { get; set; } = null!;
    }
}
