using System;
using System.Collections.Generic;

namespace lab2.db.classes;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<PlanStage> PlanStages { get; set; } = new List<PlanStage>();
}
