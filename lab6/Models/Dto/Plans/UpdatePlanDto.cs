/*
 *       @Author: yaile
 */

namespace lab6.Models.Dto.Plans;

public class UpdatePlanDto
{
    public string PlanName { get; set; } = null!;
    public int DirectionId { get; set; }
    public int UserId { get; set; }
}