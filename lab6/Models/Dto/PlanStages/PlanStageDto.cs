/*
 *       @Author: yaile
 */

namespace lab6.Models.Dto.PlanStages;

public class PlanStageDto
{
    public int StageID { get; set; }
    public string StageName { get; set; } = null!;
    public string PlanName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public int Priority { get; set; }
    public DateOnly OpenDate { get; set; }
    public DateOnly? CloseDate { get; set; }
}