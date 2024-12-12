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
public class PlanStagesController : ControllerBase
{
    private readonly PlansDbContext _context;

    public PlanStagesController(PlansDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAllStages()
    {
        var stages = _context.PlanStages.Include(ps => ps.Status)
            .Include(ps => ps.Plan)
            .Select(ps => new PlanStageDto
            {
                StageID = ps.StageID,
                PlanName = ps.Plan.PlanName,
                StageName = ps.StageName,
                StatusName = ps.Status.StatusName,
                Priority = ps.Priority,
                OpenDate = ps.OpenDate,
                CloseDate = ps.CloseDate
            })
            .ToList();
        
        return Ok(stages);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetStageById(int id)
    {
        var stage = _context.PlanStages.Include(ps => ps.Status)
            .Include(ps => ps.Plan)
            .FirstOrDefault(ps => ps.StageID == id);
        
        if (stage == null)
            return NotFound();

        var stageDto = new PlanStageDto
        {
            StageID = stage.StageID,
            PlanName = stage.Plan.PlanName,
            StageName = stage.StageName,
            StatusName = stage.Status.StatusName,
            Priority = stage.Priority,
            OpenDate = stage.OpenDate,
            CloseDate = stage.CloseDate
        };
        
        return Ok(stageDto);
    }
    
    [HttpPost]
    public IActionResult AddStage(AddPlanStageDto addPlanStageDto)
    {
        var stage = new PlanStage
        {
            StageName = addPlanStageDto.StageName,
            PlanId = addPlanStageDto.PlanId,
            StatusId = addPlanStageDto.StatusId,
            Priority = addPlanStageDto.Priority,
            OpenDate = addPlanStageDto.OpenDate,
            CloseDate = addPlanStageDto.CloseDate
        };
        _context.PlanStages.Add(stage);
        _context.SaveChanges();
        
        return Ok(new PlanStageDto
        {
            StageID = stage.StageID,
            PlanName = stage.Plan.PlanName,
            StageName = stage.StageName,
            StatusName = stage.Status.StatusName,
            Priority = stage.Priority,
            OpenDate = stage.OpenDate,
            CloseDate = stage.CloseDate
        });
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateStage(int id, UpdatePlanStageDto updatePlanStageDto)
    {
        var stage = _context.PlanStages.Find(id);
        if (stage == null)
            return NotFound();
        
        stage.StageName = updatePlanStageDto.StageName;
        stage.PlanId = updatePlanStageDto.PlanId;
        stage.StatusId = updatePlanStageDto.StatusId;
        stage.Priority = updatePlanStageDto.Priority;
        stage.OpenDate = updatePlanStageDto.OpenDate;
        stage.CloseDate = updatePlanStageDto.CloseDate;
        _context.SaveChanges();
        
        return Ok();
    }
}