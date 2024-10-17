using System;
using System.Collections.Generic;

namespace lab2.db.classes;

public partial class PlanStage
{
    public int StageId { get; set; }

    public string StageName { get; set; } = null!;

    public int PlanId { get; set; }

    public int StatusId { get; set; }

    public int Priority { get; set; }

    public DateOnly OpenDate { get; set; }

    public DateOnly? CloseDate { get; set; }

    public virtual StudyPlan Plan { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
