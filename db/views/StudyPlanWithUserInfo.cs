using System;
using System.Collections.Generic;

namespace lab2.db.views;

public partial class StudyPlanWithUserInfo
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string DirectionName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;
}
