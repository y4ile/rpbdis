namespace lab4.Models
{
    public class PlanStage
    {
        public int StageID { get; set; }

        public string StageName { get; set; } = null!;

        public int PlanId { get; set; }

        public int StatusId { get; set; }

        public int Priority { get; set; }

        public DateOnly OpenDate { get; set; }

        public DateOnly? CloseDate { get; set; }

        public virtual StudyPlan Plan { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;
    }
}
