namespace lab4.Models
{
    public class Status
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; } = null!;

        public virtual ICollection<PlanStage> PlanStages { get; set; } = new List<PlanStage>();
    }
}
