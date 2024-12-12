using System.ComponentModel.DataAnnotations;

namespace lab6.Models.Entities
{
    public class Status
    {
        [Required]
        [Key]
        public int StatusId { get; set; }
        
        [StringLength(50)]
        public string StatusName { get; set; } = null!;

        public virtual ICollection<PlanStage> PlanStages { get; set; } = new List<PlanStage>();
    }
}
