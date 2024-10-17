using System;
using System.Collections.Generic;

namespace lab2.db.classes;

public partial class StudyPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public int DirectionId { get; set; }

    public int UserId { get; set; }

    public virtual DevelopmentDirection Direction { get; set; } = null!;

    public virtual ICollection<PlanStage> PlanStages { get; set; } = new List<PlanStage>();

    public virtual User User { get; set; } = null!;
}
