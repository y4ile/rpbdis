using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab6.Models.Entities
{
    public class PlanStage
    {
        [Required]
        [Key]
        public int StageID { get; set; }

        [StringLength(50)]
        public string StageName { get; set; } = null!;
        
        [ForeignKey("StudyPlan")]
        public int PlanId { get; set; }
        
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public int Priority { get; set; }
    
        public DateOnly OpenDate { get; set; }

        public DateOnly? CloseDate { get; set; }

        public virtual StudyPlan Plan { get; set; } = null!;

        public virtual Status Status { get; set; } = null!;
    }
}
