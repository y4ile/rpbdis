using System;
using System.Collections.Generic;

namespace lab2.db.views;

public partial class PlanStagesWithInfo
{
    public int StageId { get; set; }

    public string StageName { get; set; } = null!;

    public string PlanName { get; set; } = null!;

    public string StatusName { get; set; } = null!;

    public int Priority { get; set; }

    public DateOnly OpenDate { get; set; }

    public DateOnly? CloseDate { get; set; }
}
