using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab6.Models.Entities
{
    public class StudyPlan
    {
        [Required]
        [Key]
        public int PlanID { get; set; }
        
        [StringLength(50)]
        public string PlanName { get; set; } = null!;
        
        [ForeignKey("DevelopmentDirection")]
        public int DirectionId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual DevelopmentDirection Direction { get; set; } = null!;

        public virtual ICollection<PlanStage> PlanStages { get; set; } = new List<PlanStage>();

        public virtual User User { get; set; } = null!;
    }
}
