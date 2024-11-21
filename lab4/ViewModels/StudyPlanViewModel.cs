using lab4.Models;

namespace lab4.ViewModels
{
    public class StudyPlanViewModel
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public string DirectionName { get; set; }
        public string UserLogin { get; set; }
        public int NumberOfStages { get; set; }
    }
}
