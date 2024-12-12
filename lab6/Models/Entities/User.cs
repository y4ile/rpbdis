using System.ComponentModel.DataAnnotations;

namespace lab6.Models.Entities
{
    public class User
    {
        [Required]
        [Key]
        public int UserID { get; set; }
        
        [StringLength(50)]
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
        
        [StringLength(50)]
        public string Email { get; set; } = null!;

        public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    }
}
