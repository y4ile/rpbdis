/*
 *       @Author: yaile
 */

using lab6.Data;
using lab6.Models.Dto.Plans;
using lab6.Models.Dto.PlanStages;
using lab6.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudyPlansController : ControllerBase
{
    private readonly PlansDbContext _context;

    public StudyPlansController(PlansDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAllPlans()
    {
        var plans = _context.StudyPlans.Include(sp => sp.Direction)
            .Include(sp => sp.User)
            .Include(sp => sp.PlanStages)
            .ThenInclude(ps => ps.Status)
            .Select(sp => new PlanDto()
            {
                PlanName = sp.PlanName,
                DirectionName = sp.Direction.DirectionName,
                Username = sp.User.Login,
                Stages = sp.PlanStages.Select(ps => new PlanStageDto()
                {
                    StageID = ps.StageID,
                    PlanName = ps.Plan.PlanName,
                    StageName = ps.StageName,
                    StatusName = ps.Status.StatusName,
                    Priority = ps.Priority,
                    OpenDate = ps.OpenDate,
                    CloseDate = ps.CloseDate
                }).ToList(),
            })
            .ToList();
        
        return Ok(plans);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetPlanById(int id)
    {
        var plan = _context.StudyPlans.Include(sp => sp.Direction)
            .Include(sp => sp.User)
            .Include(sp => sp.PlanStages)
            .ThenInclude(ps => ps.Status)
            .FirstOrDefault(sp => sp.PlanID == id);
        
        if (plan == null)
            return NotFound();

        var planDto = new PlanDto
        {
            PlanName = plan.PlanName,
            DirectionName = plan.Direction.DirectionName,
            Username = plan.User.Login,
            Stages = plan.PlanStages.Select(ps => new PlanStageDto
            {
                StageID = ps.StageID,
                PlanName = ps.Plan.PlanName,
                StageName = ps.StageName,
                StatusName = ps.Status.StatusName,
                Priority = ps.Priority,
                OpenDate = ps.OpenDate,
                CloseDate = ps.CloseDate
            }).ToList(),
        };
        
        return Ok(planDto);
    }

    [HttpPost]
    public IActionResult AddPlan(AddPlanDto addPlanDto)
    {
        var plan = new StudyPlan
        {
            PlanName = addPlanDto.PlanName,
            UserId = addPlanDto.UserId,
            DirectionId = addPlanDto.DirectionId
        };
        _context.StudyPlans.Add(plan);
        _context.SaveChanges();
        
        return Ok(new PlanDto
        {
            PlanName = plan.PlanName,
            DirectionName = plan.Direction.DirectionName,
            Username = plan.User.Login,
            Stages = plan.PlanStages.Select(ps => new PlanStageDto()
            {
                StageID = ps.StageID,
                PlanName = ps.Plan.PlanName,
                StageName = ps.StageName,
                StatusName = ps.Status.StatusName,
                Priority = ps.Priority,
                OpenDate = ps.OpenDate,
                CloseDate = ps.CloseDate
            }).ToList()
        });
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdatePlan(int id, UpdatePlanDto updatePlanDto)
    {
        var plan = _context.StudyPlans.Find(id);
        if (plan == null)
            return NotFound();
        
        plan.PlanName = updatePlanDto.PlanName;
        plan.DirectionId = updatePlanDto.DirectionId;
        plan.UserId = updatePlanDto.UserId;
        _context.SaveChanges();
        
        return Ok();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeletePlan(int id)
    {
        var plan = _context.StudyPlans.Find(id);
        if (plan == null)
            return NotFound();
        
        _context.StudyPlans.Remove(plan);
        _context.SaveChanges();
        return Ok();
    }
}