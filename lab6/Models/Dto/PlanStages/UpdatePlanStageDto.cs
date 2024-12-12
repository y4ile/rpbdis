/*
 *       @Author: yaile
 */

namespace lab6.Models.Dto.PlanStages;

public class UpdatePlanStageDto
{
    public string StageName { get; set; } = null!;
    public int PlanId { get; set; }
    public int StatusId { get; set; }
    public int Priority { get; set; }
    public DateOnly OpenDate { get; set; }
    public DateOnly? CloseDate { get; set; }
}