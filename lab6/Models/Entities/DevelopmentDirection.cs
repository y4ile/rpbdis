using System.ComponentModel.DataAnnotations;

namespace lab6.Models.Entities
{
    public class DevelopmentDirection
    {
        [Required]
        [Key]
        public int DirectionID { get; set; }
        
        [StringLength(50)]
        public string DirectionName { get; set; } = null!;
    
        public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    }
}
