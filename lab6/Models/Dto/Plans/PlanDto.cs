/*
 *       @Author: yaile
 */

using lab6.Models.Dto.PlanStages;
using lab6.Models.Entities;

namespace lab6.Models.Dto.Plans;

public class PlanDto
{
    public string PlanName { get; set; } = null!;
    public string DirectionName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public List<PlanStageDto> Stages { get; set; } = null!;
}