using System;
using System.Collections.Generic;

namespace lab2.db.classes;

public partial class DevelopmentDirection
{
    public int DirectionId { get; set; }

    public string DirectionName { get; set; } = null!;

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
}
